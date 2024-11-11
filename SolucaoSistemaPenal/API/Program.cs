using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

builder.Services.AddCors(options =>
options.AddPolicy("Acesso Total",
    configs => configs
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    )
);

var app = builder.Build();


app.MapGet("/", () => "API de Sistema Penitencial");

// CRUD: detento

// criar: POST
app.MapPost("/api/detento/cadastrar", ([FromBody] Detento detento, [FromServices] AppDataContext ctx) =>
{

    ctx.TabelaDetentos.Add(detento);
    ctx.SaveChanges();
    return Results.Created("", detento);
});

// cadastrar Lista de detentos : POST
app.MapPost("/api/detento/cadastrar/lista", ([FromBody] List<Detento> detentos, [FromServices] AppDataContext ctx) =>
{
    if (detentos == null || detentos.Count == 0)
    {
        return Results.NotFound("A lista de detentos está vazia ou é inválida");
    }
    ctx.TabelaDetentos.AddRange(detentos);
    ctx.SaveChanges();

    return Results.Created("", detentos.ToList());
});

// listar: GET
app.MapGet("/api/detento/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.TabelaDetentos.Any())
    {
        List<Detento> detentos = [];
        foreach (Detento detento in ctx.TabelaDetentos.ToList())
        {
            var atividades = ctx.TabelaAtividades.Where(x => x.DetentoId == detento.DetentoId);
            detento.Atividades = atividades.ToList();
            detentos.Add(detento);
        }
        return Results.Ok(detentos);
        /* a forma acima funcionou melhor que a de baixo: 
        List<Detento> detentos = ctx.TabelaDetentos.ToList();
        porque, na de cima, a lista de atividades não constava em detento */
    }
    return Results.NotFound();
});

// buscar (id): GET
app.MapGet("/api/detento/buscar/id:{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Detento? detento = ctx.TabelaDetentos.Find(id);
    var atividades = ctx.TabelaAtividades.Where(x => x.DetentoId == id);
    if (detento == null)
    {
        return Results.NotFound();
    }
    detento.Atividades = atividades.ToList();
    return Results.Ok(detento);
});

app.MapGet("/api/detento/buscar/cpf:{cpf}", ([FromRoute] string cpf, [FromServices] AppDataContext ctx) =>
{
    Detento? detento = ctx.TabelaDetentos.FirstOrDefault(x => x.CPF == cpf);
    if (detento == null)
    {
        return Results.NotFound();
    }
    var atividades = ctx.TabelaAtividades.Where(x => x.DetentoId == detento.CPF);
    detento.Atividades = atividades.ToList();
    return Results.Ok(detento);
});

// alterar (id): PUT
app.MapPut("/api/detento/alterar/{id}", ([FromRoute] string id, [FromBody] Detento detentoAlterado, [FromServices] AppDataContext ctx) =>
{
    var detento = ctx.TabelaDetentos.Find(id);
    if (detento is null)
    {
        return Results.NotFound();
    }

    detento.Nome = detentoAlterado.Nome;
    detento.Sexo = detentoAlterado.Sexo;
    detento.CPF = detentoAlterado.CPF;
    detento.PenaRestante = detentoAlterado.PenaRestante;
    detento.FimPena = detentoAlterado.FimPena;

    ctx.TabelaDetentos.Update(detento);
    ctx.SaveChanges();
    return Results.Ok(detento);
});

// deletar (id): DELETE
app.MapDelete("/api/detento/deletar/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Detento? detento = ctx.TabelaDetentos.Find(id);
    if (detento is null)
    {
        return Results.NotFound();
    }
    ctx.TabelaDetentos.Remove(detento);
    ctx.SaveChanges();

    return Results.Ok(detento);
});

// listar atividades de um detento: GET 
app.MapGet("/api/atividade/listar/detento/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Detento? detento = ctx.TabelaDetentos.Find(id);
    var atividades = ctx.TabelaAtividades.Where(x => x.DetentoId == id);
    if (detento is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(atividades);
});

// cadastrar atividades especifica ou todas evitando conflito: POST
app.MapPost("/api/atividade/detento/cadastrar/{id}/{nomeAtividade}", ([FromRoute] string id, [FromRoute] string nomeAtividade, [FromServices] AppDataContext ctx) =>
{
    var detento = ctx.TabelaDetentos.Find(id);

    if (detento is null)
    {
        return Results.NotFound("Detento não encontrado.");
    }

    detento.Atividades = ctx.TabelaAtividades.Where(x => x.DetentoId == id).ToList();

    if (nomeAtividade.ToLower() == "leitura")
    {
        //verificando se ja existe LEITURA atribuido a esse detento
        if (detento.Atividades.Any(x => x is Leitura))
        {
            return Results.Conflict("Atividade LEITURA já existente nesse detento");
        }
        Atividade leitura = new Leitura();
        leitura.Tipo = "Leitura";

        leitura.DetentoId = detento.DetentoId;
        ctx.TabelaAtividades.Add(leitura);
        ctx.SaveChanges();
        return Results.Created("", leitura);
    }
    else if (nomeAtividade.ToLower() == "estudo")
    {
        //verificando se ja existe ESTUDO atribuido a esse detento
        if (detento.Atividades.Any(x => x is Estudo))
        {
            return Results.Conflict("Atividade ESTUDO já existente nesse detento");
        }
        Atividade estudo = new Estudo();
        estudo.Tipo = "Estudo";
        estudo.DetentoId = detento.DetentoId;
        ctx.TabelaAtividades.Add(estudo);
        ctx.SaveChanges();
        return Results.Created("", estudo);

    }
    else if (nomeAtividade.ToLower() == "trabalho")
    {
        //verificando se ja existe TRABALHO atribuido a esse detento
        if (detento.Atividades.Any(x => x is Trabalho))
        {
            return Results.Conflict("Atividade TRABALHO já existente nesse detento");
        }
        Atividade trabalho = new Trabalho();
        trabalho.Tipo = "Trabalho";
        trabalho.DetentoId = detento.DetentoId;
        ctx.TabelaAtividades.Add(trabalho);
        ctx.SaveChanges();
        return Results.Created("", trabalho);
    }
    else if (nomeAtividade.ToLower() == "todos")
    {
        List<Atividade> atividades = [];

        //se o detento tiver uma das atividades já cadastradas ela não será cadastrada novamente
        if (!detento.Atividades.Any(x => x is Leitura))
        {
            atividades.Add(new Leitura { DetentoId = detento.DetentoId });
        }
        if (!detento.Atividades.Any(x => x is Estudo))
        {
            atividades.Add(new Estudo { DetentoId = detento.DetentoId });
        }
        if (!detento.Atividades.Any(x => x is Trabalho))
        {
            atividades.Add(new Trabalho { DetentoId = detento.DetentoId });
        }
        if (atividades.Count() == 0)
        {
            return Results.Conflict("Nenhuma atividade a ser Registrada");
        }
        ctx.TabelaAtividades.AddRange(atividades);
        ctx.SaveChanges();
        return Results.Created("", atividades);
    }
    else
    {
        return Results.NotFound("Tipo de Atividade não encontrada: " + nomeAtividade.ToLower() + "(Aceitas: leitura, trabalho, estudo, todos)");
    }
});

// alterar atividade: PUT
app.MapPut("/api/atividade/alterar/{idAtividade}", ([FromBody] Leitura atividadeAlterada, [FromRoute] string idAtividade, [FromServices] AppDataContext ctx) =>
{
    var atividade = ctx.TabelaAtividades.Find(idAtividade);

    if (atividade is null)
    {
        return Results.NotFound("Atividade não encontrada.");
    }

    if (atividade is Leitura leitura)
    {
        if (leitura.AnoAtual != DateTime.Now.Year)
        {
            atividade.Contador = 0;
            leitura.AnoAtual = DateTime.Now.Year;
        }
        leitura.Limite = atividadeAlterada.Limite;
        ctx.TabelaAtividades.Update(atividade);
        ctx.SaveChanges();
        return Results.Ok(atividade);
    }

    //unico tipo de alteracao que atividade faz é aumentar o contador, o que permite que façamos:
    atividade.Contador++;
    ctx.TabelaAtividades.Update(atividade);
    ctx.SaveChanges();
    return Results.Ok(atividade);
});

// CRUD: funcionário 

// criar: POST
app.MapPost("/api/funcionario/cadastrar", ([FromBody] Funcionario funcionario, [FromServices] AppDataContext ctx) =>
{

    ctx.TabelaFuncionarios.Add(funcionario);
    ctx.SaveChanges();
    return Results.Created("", funcionario);
});

// cadastrar Lista de funcionarios (facilitar testes): POST
app.MapPost("/api/funcionario/cadastrar/lista", ([FromBody] List<Funcionario> funcionarios, [FromServices] AppDataContext ctx) =>
{
    if (funcionarios == null || funcionarios.Count == 0)
    {
        return Results.NotFound("A lista de funcionários está vazia ou é inválida");
    }
    ctx.TabelaFuncionarios.AddRange(funcionarios);
    ctx.SaveChanges();

    return Results.Created("", funcionarios.ToList());
});

// listar: GET
app.MapGet("/api/funcionario/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.TabelaFuncionarios.Any())
    {
        List<Funcionario> funcionarios = ctx.TabelaFuncionarios.ToList();
        return Results.Ok(funcionarios);
    }
    return Results.NotFound();
});

// buscar (id): GET
app.MapGet("/api/funcionario/buscar/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Funcionario? funcionario = ctx.TabelaFuncionarios.Find(id);
    if (funcionario == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(funcionario);
});

// alterar (id): PUT
app.MapPut("/api/funcionario/alterar/{id}", ([FromRoute] string id, [FromBody] Funcionario funcionarioAlterado, [FromServices] AppDataContext ctx) =>
{
    var funcionario = ctx.TabelaFuncionarios.Find(id);
    if (funcionario is null)
    {
        return Results.NotFound();
    }

    funcionario.Nome = funcionarioAlterado.Nome;
    funcionario.Sexo = funcionarioAlterado.Sexo;
    funcionario.CPF = funcionarioAlterado.CPF;
    funcionario.Cargo = funcionarioAlterado.Cargo;

    ctx.TabelaFuncionarios.Update(funcionario);
    ctx.SaveChanges();
    return Results.Ok(funcionario);
});

// deletar (id): DELETE
app.MapDelete("/api/funcionario/deletar/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Funcionario? funcionario = ctx.TabelaFuncionarios.Find(id);
    if (funcionario is null)
    {
        return Results.NotFound();
    }
    ctx.TabelaFuncionarios.Remove(funcionario);
    ctx.SaveChanges();

    return Results.Ok(funcionario);
});

app.MapPut("/api/arrumarTiposAtividade", ([FromServices] AppDataContext ctx) =>
{
    var atividades = ctx.TabelaAtividades.ToList();

    foreach (Atividade atividade in atividades)
    {
        if (atividade is Leitura)
        {
            atividade.Tipo = "Leitura";
        }
        else if (atividade is Estudo)
        {
            atividade.Tipo = "Estudo";
        }
        else if (atividade is Trabalho)
        {
            atividade.Tipo = "Trabalho";
        }

    }
    ctx.TabelaAtividades.UpdateRange(atividades);
    ctx.SaveChanges();
    
    return Results.Ok(atividades);
});

app.UseCors("Acesso Total");
app.Run();