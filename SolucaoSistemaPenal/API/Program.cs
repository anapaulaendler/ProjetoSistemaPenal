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
    detento.Atividades.AddRange([
        new Leitura{ Detento = detento, DetentoId = detento.Id},
        new Estudo{ Detento = detento, DetentoId = detento.Id}, 
        new Trabalho{ Detento = detento, DetentoId = detento.Id}]);
    ctx.TabelaDetentos.Add(detento);
    ctx.SaveChanges();
    return Results.Created("", detento);
});

// listar: GET
app.MapGet("/api/detento/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.TabelaDetentos.Count() > 0)
    {
        return Results.Ok(ctx.TabelaDetentos.ToList());
    }
    return Results.NotFound();
});

// buscar (cpf): GET
app.MapGet("/api/buscar/detento/{cpf}", ([FromRoute] string cpf, [FromServices] AppDataContext ctx) =>
{
    Detento? detento = ctx.TabelaDetentos.Find(cpf);
    if (detento == null)
    {
        return Results.NotFound();
    }
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

// CRUD: atividade

// criar: POST
app.MapPost("/api/atividade/cadastrar/leitura/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Detento? detento = ctx.TabelaDetentos.Find(id);
    if (detento is null)
    {
        return Results.NotFound();
    }

    Atividade atividade = new Leitura { Detento = detento, DetentoId = id };
    // detento.Atividades.Add(atividade);
    detento.Atividades.Add(atividade);
    ctx.TabelaDetentos.Update(detento);
    ctx.SaveChanges();
    return Results.Created("", atividade);
});

//PEDRO - Aqui tem que ser listar a atividade de um detento em específico
// listar: GET 
// app.MapGet("/api/atividade/listar", ([FromServices] AppDataContext ctx) =>
// {
//     if (ctx.TabelaAtividades.Count() > 0)
//     {
//         return Results.Ok(ctx.TabelaAtividades.ToList());
//     }
//     return Results.NotFound();
// });


//PEDRO - o certo não seria buscar por ID?
// buscar (nome): GET
// app.MapGet("/api/atividade/buscar/{nome}", ([FromRoute] string nome, [FromServices] AppDataContext ctx) =>
// {
//     Atividade? atividade = ctx.TabelaAtividades.Find(nome);
//     if (atividade == null)
//     {
//         return Results.NotFound();
//     }
//     return Results.Ok(ctx.TabelaAtividades.ToList());
// });

//PEDRO - o certo não seria alterar por ID?
// alterar (nome): PUT
// app.MapPut("/api/atividade/alterar/{nome}", (
//     [FromRoute] string nome,
//     [FromBody] Atividade atividadeAlterada,
//     [FromServices] AppDataContext ctx) =>
// {
//     Atividade? atividade = ctx.TabelaAtividades.Find(nome);
//     if (atividade == null)
//     {
//         return Results.NotFound();
//     }

// atividade.Id = atividadeAlterada.Id;
// atividade.Nome = atividadeAlterada.Nome;
// atividade.Contador = atividadeAlterada.Contador;
// atividade.Equivalencia = atividadeAlterada.Equivalencia;
// atividade.AnoAtual = atividadeAlterada.AnoAtual;
// atividade.Limite = atividadeAlterada.Limite;

//     ctx.TabelaAtividades.Update(atividade);
//     ctx.SaveChanges();
//     return Results.Ok(atividade);
// });

//PEDRO - o certo não seria deletar por ID?
// deletar (nome): DELETE
// app.MapDelete("/api/atividade/deletar/{nome}", ([FromRoute] string nome, [FromServices] AppDataContext ctx) =>
// {
//     Atividade? atividade = ctx.TabelaAtividades.Find(nome);
//     if (atividade == null)
//     {
//         return Results.NotFound();
//     }
//     ctx.TabelaAtividades.Remove(atividade);
//     ctx.SaveChanges();
//     return Results.Ok(atividade);
// });

app.Run();