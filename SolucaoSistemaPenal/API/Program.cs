using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
List<Atividade> atividades = [];
List<Detento> detentos = [];

app.MapGet("/", () => "API de Sistema Penitencial");

// CRUD: detento

// criar: POST
app.MapPost("/api/detento/cadastrar", ([FromBody] Detento detento) =>
{
    detentos.Add(detento);
    return Results.Created("", detento);
});

// listar: GET
app.MapGet("/api/detento/listar", () =>
{
    if (detentos.Count > 0)
    {
        return Results.Ok(detentos);
    }
    return Results.NotFound();
});

// buscar (nome): GET
app.MapGet("/api/detento/buscar/{nome}", ([FromRoute] string nome) =>
{
    Detento? detento = detentos.Find(x => x.Nome == nome);
    if (detento == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(detento);
});

// buscar (cpf): GET
app.MapGet("/api/buscar/detento/{cpf}", ([FromRoute] string cpf) =>
{
    Detento? detento = detentos.Find(x => x.CPF == cpf);
    if (detento == null) 
    {
        return Results.NotFound();
    }
    return Results.Ok(detento);
});

// alterar (cpf): PUT
app.MapPut("/api/detento/alterar/{cpf}", ([FromRoute] string cpf, [FromBody] Detento detentoAlterado) =>
{
    Detento? detento = detentos.Find(x => x.CPF == cpf);
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
    return Results.Ok(detento);
});

// deletar (cpf): DELETE
app.MapDelete("/api/detento/deletar/{cpf}", ([FromRoute] string cpf) =>
{
    Detento? detento = detentos.Find(x => x.CPF == cpf);
    if (detento == null)
    {
        return Results.NotFound();
    }
    detentos.Remove(detento);
    return Results.Ok(detento);
});

// CRUD: atividade