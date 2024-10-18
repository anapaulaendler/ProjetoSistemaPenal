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
app.MapGet("/api/detento/buscar/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
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

// cadastrar atividades especifica ou todas evitando conflito: GET
app.MapPost("/api/detento/atividade/cadastrar/{id}/{nomeAtividade}", ([FromRoute] string id, [FromRoute] string nomeAtividade, [FromServices] AppDataContext ctx) =>
{
    var detento = ctx.TabelaDetentos.Find(id);

    if (detento is null)
    {
        return Results.NotFound("Detento não encontrado.");
    }

    detento.Atividades = ctx.TabelaAtividades.Where(x => x.DetentoId == id).ToList();
    
        if(nomeAtividade.ToLower() == "leitura")
        {
            //verificando se ja existe LEITURA atribuido a esse detento
            if(detento.Atividades.Any(x => x is Leitura))
            {
                return Results.Conflict("Atividade LEITURA já existente nesse detento");
            }
            Atividade leitura = new Leitura();
            leitura.DetentoId = detento.DetentoId;
            // ctx.TabelaAtividades.Add(leitura);
            // ctx.SaveChanges();
            return Results.Created("", leitura);
        }
        else if(nomeAtividade.ToLower() == "estudo")
        {
            //verificando se ja existe ESTUDO atribuido a esse detento
            if(detento.Atividades.Any(x => x is Estudo))
            {
                return Results.Conflict("Atividade ESTUDO já existente nesse detento");
            }
            Atividade estudo = new Estudo();
            estudo.DetentoId = detento.DetentoId;
            ctx.TabelaAtividades.Add(estudo);
            ctx.SaveChanges();
            return Results.Created("", estudo);

        }
        else if(nomeAtividade.ToLower() == "trabalho")
        {
            //verificando se ja existe TRABALHO atribuido a esse detento
            if(detento.Atividades.Any(x => x is Trabalho))
            {
                return Results.Conflict("Atividade TRABALHO já existente nesse detento");
            }
            Atividade trabalho = new Trabalho();
            trabalho.DetentoId = detento.DetentoId;
            ctx.TabelaAtividades.Add(trabalho);
            ctx.SaveChanges();
            return Results.Created("", trabalho);
        }
        else if(nomeAtividade.ToLower() == "todas")
        {
            List<Atividade> atividades = [];

            //se o detento tiver uma das atividades já cadastradas ela não será cadastrada novamente
            if(!detento.Atividades.Any(x => x is Leitura))
            {
                atividades.Add(new Leitura{ DetentoId = detento.DetentoId});
            }
            if(!detento.Atividades.Any(x => x is Estudo))
            {
                atividades.Add(new Estudo{ DetentoId = detento.DetentoId});
            }
            if(!detento.Atividades.Any(x => x is Trabalho))
            {
                atividades.Add(new Trabalho{ DetentoId = detento.DetentoId});
            }
            ctx.TabelaAtividades.AddRange(atividades);
            ctx.SaveChanges();
            return Results.Created("", atividades);
        }
        else
        {
            return Results.NotFound("Tipo de Atividade não encontrada: " + nomeAtividade.ToLower() + "(Aceitas: leitura, trabalho, estudo, todas)");
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
        if (atividadeBuscar.AtividadeId == idAtividade)
        {
            atividade = atividadeBuscar;
            break;
        }
    }

    if (atividade is null)
    {
        return Results.NotFound("Atividade não encontrada.");
    }

    // if (atividade is Leitura leitura)
    // {
    //     if (leitura.AnoAtual != DateTime.Now.Year)
    //     {
    //         leitura.Contador = 0;
    //         leitura.AnoAtual = DateTime.Now.Year;
    //     }

         // vai dar erro depois, ver melhor como implementar a lógica
    // }

    //unico tipo de alteracao que atividade faz é aumentar o contador, o que permite que façamos:
    atividade.Contador++;
    ctx.TabelaAtividades.Update(atividade);
    ctx.SaveChanges();
    return Results.Ok(atividade);
});

app.Run();