using Microsoft.AspNetCore.Cors;
using MinimalAPIPeliculas.Entidades;

var builder=WebApplication.CreateBuilder(args);
var origenesPermitidos = builder.Configuration.GetValue<string>("originespermitidos")!;


//var apellido = builder.Configuration.GetValue<string>("apellido");
//Inicio de area de los servicios

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(configuracion =>
    {
        configuracion.WithOrigins("originespermitidos").AllowAnyHeader().AllowAnyMethod();
    });

    opciones.AddPolicy("libre",configuracion =>
    {
        configuracion.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddOutputCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//FIN DE AREA DE LOS SERVICIOS
var app = builder.Build();

//inicio de area de los middleweare

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseOutputCache();

app.MapGet("/", () => "hola mundo en mayuscula");


//endpoint lo de arriba
app.MapGet("/otra-cosa", () => "Hola mundo en minuscula");
// dentro de las comillas "/" la / es la ruta, luego en la url, se tiene q poner el link y la ruta

// lo que esta dentro de las "/..." para que te redireccione a lo que pusiste despues de => "..."
app.MapGet ("/generos", () =>
{
    var generos = new List<Genero>
    // El tipo de dato es lo que esta entre <>
    {
        new Genero
        {
            Id = 1,
            Nombre = "Drama"
        },
        new Genero
        {
            Id = 2,
            Nombre = "Accion"
        },
        new Genero
        {
            Id = 3,
            Nombre = "Comedia"
        }

    };
    return generos;
}
//Para mostrar una variable dentro del app.mapget se encierra dentro de un {...}
);

//fin de area de los middleweare

app.Run();

 //El server almacena informacion temporal, el usuario toma esa informacion, el navegador toma como suposicion que es el lo mismo

 //El CORS regla de seguridad, sino esta no funciona, acepta cualquier cosa, desde donde hace los pedidos

 //El cache se habilita desde la segunda vez, para que sea mas rapido, no se habilita si no se necesita utilizarse

 //