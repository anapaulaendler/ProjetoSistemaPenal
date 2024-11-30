using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

// criar detento
app.MapPost("/api/detento/cadastrar", ([FromBody] Detento detento, [FromServices] AppDataContext ctx) =>
{
    if(ctx.TabelaDetentos.FirstOrDefault(x => x.CPF == detento.CPF) != null)
    {
        return Results.Conflict("CPF já registrado.");
    }
    ctx.TabelaDetentos.Add(detento);
    ctx.SaveChanges();
    return Results.Created("", detento);
});

// criar lista de detentos
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

// listar detentos
app.MapGet("/api/detento/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.TabelaDetentos.Any())
    {
        // List<Detento> detentos = [];
        // foreach (Detento detento in ctx.TabelaDetentos.ToList())
        // {
        //     var atividades = ctx.TabelaAtividades.Where(x => x.DetentoId == detento.DetentoId);
        //     detento.Atividades = atividades.ToList();
        //     detentos.Add(detento);
        // }
        List<Detento> detentos = ctx.TabelaDetentos.Include(x => x.Atividades).ToList();
        return Results.Ok(detentos);
    }
    return Results.NotFound();
});

// buscar detento (id e cpf embaixo)
app.MapGet("/api/detento/buscar/id:{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Detento? detento = ctx.TabelaDetentos.Find(id);

    if (detento == null)
    {
        return Results.NotFound();
    }

    var atividades = ctx.TabelaAtividades.Where(x => x.DetentoId == id);
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

    var atividades = ctx.TabelaAtividades.Where(x => x.DetentoId == detento.DetentoId);
    detento.Atividades = atividades.ToList();
    return Results.Ok(detento);
});

// alterar detento
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
    detento.DataNascimento = detentoAlterado.DataNascimento;
    // detento.PenaRestante = detentoAlterado.PenaRestante;
    detento.FimPena = detentoAlterado.FimPena;

    ctx.TabelaDetentos.Update(detento);
    ctx.SaveChanges();
    return Results.Ok(detento);
});

// deletar detento
app.MapDelete("/api/detento/deletar/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Detento? detento = ctx.TabelaDetentos.Find(id);
    if (detento is null)
    {
        return Results.NotFound();
    }
    
    DetentoInativo dInativo = new DetentoInativo();

    dInativo.DetentoInativoId = detento.DetentoId;
    dInativo.CPF = detento.CPF;
    dInativo.DataNascimento = detento.DataNascimento;
    dInativo.FimPena = detento.FimPena;
    dInativo.InicioPena = detento.InicioPena;
    dInativo.Nome = detento.Nome;
    dInativo.Sexo = detento.Sexo;

    ctx.TabelaDetentosInativos.Add(dInativo);
    ctx.TabelaDetentos.Remove(detento);
    ctx.SaveChanges();

    return Results.Ok(detento);
});

// listar atividades de um detento: GET 
app.MapGet("/api/atividade/listar/detento/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Detento? detento = ctx.TabelaDetentos.Find(id);
    if (detento is null)
    {
        return Results.NotFound();
    }
    var atividades = ctx.TabelaAtividades.Where(x => x.DetentoId == id);

    foreach (Atividade atividade in atividades)
    {
        if (atividade is Leitura)
        {

        }
    }

    if (atividades is null)
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
        Atividade leitura = new Leitura
        {
            Tipo = "Leitura",
            DetentoId = detento.DetentoId,
            Equivalencia = 0.25,
            AnoAtual = DateTime.Now.Year,
            Limite = 12
        };
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
        Atividade estudo = new Estudo
        {
            Tipo = "Estudo",
            DetentoId = detento.DetentoId,
            Equivalencia = 3
        };
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
        Atividade trabalho = new Trabalho
        {
            DetentoId = detento.DetentoId,
            Tipo = "Trabalho",
            Equivalencia = 3
        };
        ctx.TabelaAtividades.Add(trabalho);
        ctx.SaveChanges();
        return Results.Created("", trabalho);
    }
    else if (nomeAtividade.ToLower() == "todas")
    {
        List<Atividade> atividades = [];

        //se o detento tiver uma das atividades já cadastradas ela não será cadastrada novamente
        if (!detento.Atividades.Any(x => x is Leitura))
        {
            atividades.Add(new Leitura
            {
                DetentoId = detento.DetentoId,
                Tipo = "Leitura",
                Equivalencia = 0.25,
                AnoAtual = DateTime.Now.Year,
                Limite = 12
            });

        }
        if (!detento.Atividades.Any(x => x is Estudo))
        {
            atividades.Add(new Estudo
            {
                DetentoId = detento.DetentoId,
                Tipo = "Estudo",
                Equivalencia = 3
            });
        }
        if (!detento.Atividades.Any(x => x is Trabalho))
        {
            atividades.Add(new Trabalho
            {
                DetentoId = detento.DetentoId,
                Tipo = "Trabalho",
                Equivalencia = 3
            });
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
app.MapPut("/api/atividade/alterar/{idAtividade}", ([FromRoute] string idAtividade, [FromServices] AppDataContext ctx) =>
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
            leitura.Contador = 0;
            leitura.AnoAtual = DateTime.Now.Year;
        }

        if (leitura.Contador < leitura.Limite)
        {
            leitura.Contador++;
            Detento? detento = ctx.TabelaDetentos.Find(leitura.DetentoId);

            if (detento is null)
            {
                return Results.NotFound("Detento não encontrado");
            }

            detento.FimPena = detento.FimPena.AddDays(-3);
            ctx.TabelaDetentos.Update(detento);
            ctx.TabelaAtividades.Update(leitura);
            ctx.SaveChanges();
            return Results.Ok(leitura);
        }

        return Results.NoContent();
        // TROCAR DEPOIS
    }

    atividade.Contador++;
    
    if (atividade.Contador % 3 == 0)
    {
        Detento? detento = ctx.TabelaDetentos.Find(atividade.DetentoId);

        if (detento is null)
        {
            return Results.NotFound("Detento não encontrado");
        }
        detento.FimPena = detento.FimPena.AddDays(-1);
        ctx.TabelaDetentos.Update(detento);
    }

    ctx.TabelaAtividades.Update(atividade);
    ctx.SaveChanges();
    return Results.Ok(atividade);
});

// CRUD: funcionário 

// criar funcionário
app.MapPost("/api/funcionario/cadastrar", ([FromBody] Funcionario funcionario, [FromServices] AppDataContext ctx) =>
{
    if(ctx.TabelaDetentos.FirstOrDefault(x => x.CPF == funcionario.CPF) != null)
    {
        return Results.Conflict("CPF já registrado.");
    }
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

// listar funcionários
app.MapGet("/api/funcionario/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.TabelaFuncionarios.Any())
    {
        List<Funcionario> funcionarios = ctx.TabelaFuncionarios.ToList();
        return Results.Ok(funcionarios);
    }
    return Results.NotFound();
});

// buscar funcionário
app.MapGet("/api/funcionario/buscar/id:{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Funcionario? funcionario = ctx.TabelaFuncionarios.Find(id);
    if (funcionario == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(funcionario);
});

app.MapGet("/api/funcionario/buscar/cpf:{cpf}", ([FromRoute] string cpf, [FromServices] AppDataContext ctx) =>
{
    Funcionario? funcionario = ctx.TabelaFuncionarios.FirstOrDefault(x => x.CPF == cpf);
    if (funcionario == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(funcionario);
});

// alterar funcionário
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
    funcionario.DataNascimento = funcionarioAlterado.DataNascimento;

    ctx.TabelaFuncionarios.Update(funcionario);
    ctx.SaveChanges();
    return Results.Ok(funcionario);
});

// deletar funcionário
app.MapDelete("/api/funcionario/deletar/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Funcionario? funcionario = ctx.TabelaFuncionarios.Find(id);
    if (funcionario is null)
    {
        return Results.NotFound();
    }
    ctx.TabelaFuncionarios.Remove(funcionario);
    ctx.SaveChanges();

    return Results.NoContent();
});

// listar detentos inativos
app.MapGet("/api/detentoinativo/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.TabelaDetentosInativos.Any())
    {
        List<DetentoInativo> detentos = ctx.TabelaDetentosInativos.ToList();
        return Results.Ok(detentos);
    }
    return Results.NotFound();
});

// OUTRAS FUNCIONALIDADES!!

// // deletar detento duplicado
// app.MapDelete("/api/deletar/detentoDuplicado", ([FromServices] AppDataContext ctx) =>
// {
//     var ListaDetentos = ctx.TabelaDetentos
//         .AsEnumerable() // Carrega os dados e realiza o agrupamento na memória
//         .GroupBy(d => d.CPF) // Agrupa por CPF
//         .Where(g => g.Count() > 1) // Filtra grupos com duplicados
//         .SelectMany(g => g.Skip(1)) // Pega todos menos o primeiro de cada grupo
//         .ToList();

//     if (ListaDetentos.Any())
//     {
//         ctx.TabelaDetentos.RemoveRange(ListaDetentos);
//         ctx.SaveChanges();
//     }

//     return Results.Ok("Detentos duplicados removidos com sucesso.");
// });

// // deletar funcionário duplicado
// app.MapDelete("/api/deletar/funcionarioDuplicado", ([FromServices] AppDataContext ctx) =>
// {
//     var ListaFuncionarios = ctx.TabelaFuncionarios
//         .AsEnumerable() // Carrega os dados e realiza o agrupamento na memória
//         .GroupBy(d => d.CPF) // Agrupa por CPF
//         .Where(g => g.Count() > 1) // Filtra grupos com duplicados
//         .SelectMany(g => g.Skip(1)) // Pega todos menos o primeiro de cada grupo
//         .ToList();

//     if (ListaFuncionarios.Any())
//     {
//         ctx.TabelaFuncionarios.RemoveRange(ListaFuncionarios);
//         ctx.SaveChanges();
//     }

//     return Results.Ok("Funcionarios duplicados removidos com sucesso.");
// });

// // cadastrar todas as atividades em todos os detentos [facilitar testes]

// app.MapPost("/api/atividade/detento/cadastrar/todas", ([FromServices] AppDataContext ctx) =>
// {
//     bool hasActivities = false;

//     foreach (Detento detento in ctx.TabelaDetentos.ToList())
//     {
//         List<Atividade> atividades = new List<Atividade>();

//         if (!detento.Atividades.Any(x => x is Leitura))
//         {
//             atividades.Add(new Leitura
//             {
//                 DetentoId = detento.DetentoId,
//                 Tipo = "Leitura",
//                 Equivalencia = 0.25
//             });
//         }
//         if (!detento.Atividades.Any(x => x is Estudo))
//         {
//             atividades.Add(new Estudo
//             {
//                 DetentoId = detento.DetentoId,
//                 Tipo = "Estudo",
//                 Equivalencia = 3
//             });
//         }
//         if (!detento.Atividades.Any(x => x is Trabalho))
//         {
//             atividades.Add(new Trabalho
//             {
//                 DetentoId = detento.DetentoId,
//                 Tipo = "Trabalho",
//                 Equivalencia = 3
//             });
//         }

//         if (atividades.Any())
//         {
//             ctx.TabelaAtividades.AddRange(atividades);
//             hasActivities = true;
//         }
//     }

//     if (!hasActivities)
//     {
//         return Results.Conflict("Nenhuma atividade a ser registrada para qualquer detento.");
//     }

//     ctx.SaveChanges();
//     return Results.Created("/api/atividade/detento/cadastrar/todas", "Atividades cadastradas com sucesso.");
// });

// // arrumar tipos de atividade
// app.MapPut("/api/arrumarTiposAtividade", ([FromServices] AppDataContext ctx) =>
// {
//     var atividades = ctx.TabelaAtividades.ToList();

//     foreach (Atividade atividade in atividades)
//     {
//         if (atividade is Leitura)
//         {
//             atividade.Tipo = "Leitura";
//             atividade.Equivalencia = 0.25;
//             atividade.Limite = 12;
//             atividade.AnoAtual = DateTime.Now.Year;
//         }
//         else if (atividade is Estudo)
//         {
//             atividade.Tipo = "Estudo";
//             atividade.Equivalencia = 3;
//         }
//         else if (atividade is Trabalho)
//         {
//             atividade.Tipo = "Trabalho";
//             atividade.Equivalencia = 3;
//         }
//     }
//     ctx.TabelaAtividades.UpdateRange(atividades);
//     ctx.SaveChanges();

//     return Results.Ok(atividades);
// });

// // deletar todos os funcionários
// app.MapDelete("/api/funcionario/deletar/todos", ([FromServices] AppDataContext ctx) =>
// {
//     ctx.TabelaFuncionarios.RemoveRange(ctx.TabelaFuncionarios);
//     ctx.SaveChanges();

//     return Results.NoContent();
// });

// criar lista de detentos
app.MapPost("/api/detentoinativo/cadastrar/lista", ([FromBody] List<DetentoInativo> detentos, [FromServices] AppDataContext ctx) =>
{
    if (detentos == null || detentos.Count == 0)
    {
        return Results.NotFound("A lista de detentos está vazia ou é inválida");
    }
    ctx.TabelaDetentosInativos.AddRange(detentos);
    ctx.SaveChanges();

    return Results.Created("", detentos.ToList());
});

// tudo acima foi comentado porque essas funcionalidades foram úteis em testes, mas não devem ficar no código

app.UseCors("Acesso Total");
app.Run();