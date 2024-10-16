using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
//PEDRO - atribuindo os serviços do banco de dados ao builder
var app = builder.Build();


app.MapGet("/", () => "API de Sistema Penitencial");

// !CRUD: detento

// criar: POST
app.MapPost("/api/detento/cadastrar", ([FromBody] Detento detento, [FromServices] AppDataContext ctx) =>
{
    List<Atividade> atividades = [new Leitura{DetentoId = detento.Id},
        new Estudo{DetentoId = detento.Id},
        new Trabalho{DetentoId = detento.Id}];

    detento.Atividades.AddRange(atividades);
    ctx.TabelaDetentos.Add(detento);
    ctx.SaveChanges();
    return Results.Created("", detento);
});

// listar: GET
app.MapGet("/api/detento/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.TabelaDetentos.Any())
    {
        List<Detento> detentos = [];
        foreach (Detento detento in ctx.TabelaDetentos.ToList())
        {
            var atividades = ctx.TabelaAtividades.Where(x => x.DetentoId == detento.Id);
            detento.Atividades = atividades.ToList();
            detentos.Add(detento);
        }

        return Results.Ok(detentos);
    }
    return Results.NotFound();
});

// buscar (cpf): GET
app.MapGet("/api/buscar/detento/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
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

// alterar (cpf): PUT
app.MapPut("/api/detento/alterar/{cpf}", ([FromRoute] string cpf, [FromBody] Detento detentoAlterado, [FromServices] AppDataContext ctx) =>
{
    Detento? detento = ctx.TabelaDetentos.FirstOrDefault(x => x.CPF == cpf);
    if (detento is null)
    {
        return Results.NotFound();
    }

    detento.Nome = detentoAlterado.Nome;
    detento.DataNascimento = detentoAlterado.DataNascimento;
    detento.CPF = detentoAlterado.CPF;
    detento.Sexo = detentoAlterado.Sexo;
    detento.Id = detentoAlterado.Id;
    detento.TempoPenaInicial = detentoAlterado.TempoPenaInicial;
    detento.PenaRestante = detentoAlterado.PenaRestante;
    detento.InicioPena = detentoAlterado.InicioPena;
    detento.FimPena = detentoAlterado.FimPena;
    /* ana: gente comentando sobre um erro engraçado tava arrancando meus cabelo
    porque eu tava tratando AtividadeDetento como 1 coisa quando... eu tinha trocado...
    pra coleção antes... depois dessa eu vou mimir adeus 
    + !!!!!!!!!!! ver como colocar a coleção aqui */

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
    // Atividade? estudo = ctx.TabelaAtividades.Find(id);
    // Atividade? leitura = ctx.TabelaAtividades.Find(id);
    // Atividade? trabalho = ctx.TabelaAtividades.Find(id);

    // if (estudo == null || leitura == null || trabalho == null)
    // {
    //     return Results.NotFound();
    // } 
    // ctx.TabelaAtividades.Remove(estudo);
    // ctx.TabelaAtividades.Remove(trabalho);
    // ctx.TabelaAtividades.Remove(leitura);
    ctx.TabelaDetentos.Remove(detento);
    ctx.SaveChanges();

    return Results.Ok(detento);
});

//listar atividades de um detento: GET 
app.MapGet("/api/atividade/listar/detento:{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    if (ctx.TabelaAtividades.Count() > 0)
    {
        return Results.Ok(ctx.TabelaAtividades.Where(x => x.DetentoId == id).ToList());
    }
    return Results.NotFound();
});

//buscar atividade especifica GET
app.MapGet("/api/atividade/buscar/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Atividade? atividade = ctx.TabelaAtividades.Find(id);
    if (atividade == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(atividade);
});

// alterar atividade PUT
app.MapPut("/api/atividade/alterar/{id}", ([FromRoute] string id, [FromBody] Atividade atividadeAlterada, [FromServices] AppDataContext ctx) =>
{
    Atividade? atividade = ctx.TabelaAtividades.Find(id);
    if (atividade == null)
    {
        return Results.NotFound();
    }

    if (atividade is Leitura leitura)
    {
        leitura.Limite = ((Leitura)atividadeAlterada).Limite;
        leitura.AnoAtual = DateTime.Now.Year;
    }

    atividade.Contador = atividadeAlterada.Contador;
    ctx.TabelaAtividades.Update(atividade);
    ctx.SaveChanges();
    return Results.Ok(atividade);
});

app.Run();