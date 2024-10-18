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

// cadastrar atividades especifica: GET
app.MapPut("/api/atividade/cadastrar/IdDetento:{id}/NomeAtividade:{nomeAtividade}", ([FromRoute] string id,[FromRoute] string nomeAtividade, [FromServices] AppDataContext ctx) =>
{
    var detento = ctx.TabelaDetentos.Find(id);
    if(detento is null)
    {
        return Results.NotFound("Detento não encontrado.");
    }

    string AtividadeSelecionada = nomeAtividade.ToLower();

    switch(AtividadeSelecionada){
        case "leitura":
            Atividade leitura = new Leitura();
            leitura.DetentoId = detento.Id;
            ctx.TabelaAtividades.Add(leitura);
            ctx.SaveChanges();
            return Results.Created("", leitura);


        case "estudo" :
            Atividade estudo = new Estudo();
            estudo.DetentoId = detento.Id;
            ctx.TabelaAtividades.Add(estudo);
            ctx.SaveChanges();
            return Results.Created("", estudo);

        
        case "trabalho":
            Atividade trabalho = new Trabalho();
            trabalho.DetentoId = detento.Id;
            ctx.TabelaAtividades.Add(trabalho);
            ctx.SaveChanges();
            return Results.Created("", trabalho);

        default:
            return Results.NotFound("Tipo de Atividade não encontrada.");
    }
});

// alterar atividade: PUT
app.MapPut("/api/atividade/alterar/{idDetento}/{idAtividade}", ([FromRoute] string idDetento, [FromRoute] string idAtividade, [FromServices] AppDataContext ctx) =>
{
    Detento? detento = ctx.TabelaDetentos.Find(idDetento);
    var atividades = ctx.TabelaAtividades.Where(x => x.DetentoId == idDetento).ToList();
    if (detento is null)
    {
        return Results.NotFound("Detento não encontrado.");
    }

    Atividade? atividade = null;

    foreach (var atividadeBuscar in atividades)
    {
        if (atividadeBuscar.Id == idAtividade)
        {
            atividade = atividadeBuscar;
            break;
        }
    }

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

        // vai dar erro depois, ver melhor como implementar a lógica
    }

    //unico tipo de alteracao que atividade faz é aumentar o contador, o que permite que façamos:
    atividade.Contador++;
    ctx.TabelaAtividades.Update(atividade);
    ctx.SaveChanges();
    return Results.Ok(atividade);
});

app.Run();