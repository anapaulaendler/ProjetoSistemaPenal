using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
//PEDRO - atribuindo os serviços do banco de dados ao builder
var app = builder.Build();


app.MapGet("/", () => "API de Sistema Penitencial");

// CRUD: detento

// criar: POST
app.MapPost("/api/detento/cadastrar", ([FromBody] Detento detento, [FromServices] AppDataContext ctx) =>
{
    List<Atividade> atividades = [new Leitura{DetentoId = detento.Id},
        new Estudo{DetentoId = detento.Id},
        new Trabalho{DetentoId = detento.Id}];
        // modo de passar o id de detento para as atividades

    detento.Atividades.AddRange(atividades);
    // adiciona ao detento primeiro as atividades (esqueleto)

    ctx.TabelaDetentos.Add(detento);
    ctx.SaveChanges();
    // bd
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
        /* a forma acima funcionou melhor que a de baixo: 
        List<Detento> detentos = ctx.TabelaDetentos.ToList();
        porque, na de cima, a lista de atividades não constava em detento */
    }
    return Results.NotFound();
});

// buscar (id): GET
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

// alterar (id): PUT
app.MapPut("/api/detento/alterar/{id}", ([FromRoute] string id, [FromBody] Detento detentoAlterado, [FromServices] AppDataContext ctx) =>
{
    Detento? detento = ctx.TabelaDetentos.FirstOrDefault(x => x.Id == id);
    if (detento is null)
    {
        return Results.NotFound();
    }

    detento.Nome = detentoAlterado.Nome;
    detento.Sexo = detentoAlterado.Sexo;
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
app.MapGet("/api/atividade/listar/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Detento? detento = ctx.TabelaDetentos.Find(id);
    var atividades = ctx.TabelaAtividades.Where(x => x.DetentoId == id);
    if (detento is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(atividades);
});

// // buscar atividade especifica: GET
// app.MapGet("/api/atividade/buscar/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
// {
//     var atividade = ctx.TabelaAtividades.Find(id);
//     if (atividade is null)
//     {
//         return Results.NotFound();
//     }
//     return Results.Ok(atividade);
// });
// pra que isso

// alterar atividade: PUT
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
        // leitura.AnoAtual = DateTime.Now.Year;
        if (leitura.AnoAtual != DateTime.Now.Year)
        {
            leitura.Contador = 0;
            leitura.AnoAtual = DateTime.Now.Year;
        }
    }

    atividade.Contador = atividadeAlterada.Contador;
    ctx.TabelaAtividades.Update(atividade);
    ctx.SaveChanges();
    return Results.Ok(atividade);
});

app.Run();