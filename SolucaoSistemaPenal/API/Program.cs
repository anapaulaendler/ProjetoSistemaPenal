using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();//PEDRO - atribuindo os serviços do banco de dados ao builder
var app = builder.Build();

// PEDRO - tirei pois agora vou implementar o banco
// List<Atividade> atividades = [];
// List<Detento> detentos = [];

app.MapGet("/", () => "API de Sistema Penitencial");

// CRUD: detento

// criar: POST
app.MapPost("/api/detento/cadastrar", ([FromBody] Detento detento, [FromServices] AppDataContext ctx) =>
{
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

// buscar (nome): GET
app.MapGet("/api/detento/buscar/{nome}", ([FromRoute] string nome, [FromServices] AppDataContext ctx) =>
{
    Detento? detento = ctx.TabelaDetentos.Find(nome);
    if (detento == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(detento);
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
    Detento? detento = ctx.TabelaDetentos.Find(cpf);
    if (detento == null)
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
    detento.ListaAtividades = detentoAlterado.ListaAtividades;
    
    ctx.TabelaDetentos.Update(detento);
    ctx.SaveChanges();
    return Results.Ok(detento);
});

// deletar (cpf): DELETE
app.MapDelete("/api/detento/deletar/{cpf}", ([FromRoute] string cpf, [FromServices] AppDataContext ctx) =>
{
    Detento? detento = ctx.TabelaDetentos.Find(cpf);
    if (detento == null)
    {
        return Results.NotFound();
    }
    ctx.TabelaDetentos.Remove(detento);
    ctx.SaveChanges();
    return Results.Ok(detento);
});

// CRUD: atividade

// criar: POST
app.MapPost("/api/atividade/cadastrar", ([FromBody] Atividade atividade, [FromServices] AppDataContext ctx) =>
{
    ctx.TabelaAtividades.Add(atividade);
    ctx.SaveChanges();
    return Results.Created("", atividade);
});

// listar: GET
app.MapGet("/api/atividade/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.TabelaAtividades.Count() > 0)
    {
        return Results.Ok(ctx.TabelaAtividades.ToList());
    }
    return Results.NotFound();
});

// buscar (nome): GET
app.MapGet("/api/atividade/buscar/{nome}", ([FromRoute] string nome, [FromServices] AppDataContext ctx) =>
{
    Atividade? atividade = ctx.TabelaAtividades.Find(nome);
    if (atividade == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(ctx.TabelaAtividades.ToList());
});

// alterar (nome): PUT
app.MapPut("/api/atividade/alterar/{nome}", ([FromRoute] string nome, [FromBody] Atividade atividadeAlterada, [FromServices] AppDataContext ctx) =>
{
    Atividade? atividade = ctx.TabelaAtividades.Find(nome);
    if (atividade == null)
    {
        return Results.NotFound();
    }

    atividade.Id = atividadeAlterada.Id;
    atividade.Nome = atividadeAlterada.Nome;
    atividade.Contador = atividadeAlterada.Contador;
    atividade.Equivalencia = atividadeAlterada.Equivalencia;
    atividade.AnoAtual = atividadeAlterada.AnoAtual;
    atividade.Limite = atividadeAlterada.Limite;

    ctx.TabelaAtividades.Update(atividade);
    ctx.SaveChanges();
    return Results.Ok(atividade);
});

// deletar (nome): DELETE
app.MapDelete("/api/atividade/deletar/{nome}", ([FromRoute] string nome, [FromServices] AppDataContext ctx) =>
{
    Atividade? atividade = ctx.TabelaAtividades.Find(nome);
    if (atividade == null) {
        return Results.NotFound();
    }
    ctx.TabelaAtividades.Remove(atividade);
    ctx.SaveChanges();
    return Results.Ok(atividade);
});

// TODO: adicionar atividade em detento

// TODO: reducao de pena

