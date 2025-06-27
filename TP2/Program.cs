using Microsoft.EntityFrameworkCore;
using _2_Infraestructura;
using _2_Infraestructura.Commands;
using _2_Infraestructura.Querys;
using _3_Aplicacion.Interfaces.ICommands;
using _3_Aplicacion.Interfaces.IQuerys;
using _3_Aplicacion.Interfaces.IServices;
using _3_Aplicacion.UseCase;
using _3_Aplicacion.Dto.Response;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext con Entity Framework Core
builder.Services.AddDbContext<ProyectosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Querys
builder.Services.AddScoped<IUsuarioQuerys, UsuarioQuerys>();
builder.Services.AddScoped<IAreaQuerys, AreaQuerys>();
builder.Services.AddScoped<IStatusQuerys, StatusQuerys>();
builder.Services.AddScoped<ITypeQuerys, TypeQuerys>();
builder.Services.AddScoped<IRolQuerys, RolQuerys>();
builder.Services.AddScoped<IProyectoQuerys, ProyectoQuerys>();
builder.Services.AddScoped<IAprobacionQuerys, AprobacionQuerys>();

// Commands
builder.Services.AddScoped<IUsuarioCommands, UsuarioCommands>();
builder.Services.AddScoped<IProyectoCommands, ProyectoCommands>();
builder.Services.AddScoped<IAprobacionCommands, AprobacionCommands>();

// Generador de flujo
builder.Services.AddScoped<IFlujoAprobacionGenerator, FlujoAprobacionGenerator>();

// Servicios / UseCases
builder.Services.AddScoped<IUsuarioAutenticacionService, UsuarioAutenticacionService>();
builder.Services.AddScoped<IUsuarioRegistroService, UsuarioRegistroService>();
builder.Services.AddScoped<IUsuarioConsultaService, UsuarioConsultaService>();
builder.Services.AddScoped<IRolConsultaService, RolConsultaService>();
builder.Services.AddScoped<IAreaConsultaService, AreaConsultaService>();
builder.Services.AddScoped<ITypeConsultaService, TypeConsultaService>();
builder.Services.AddScoped<IProyectoCreacionService, ProyectoCreacionService>();
builder.Services.AddScoped<IProyectoConsultaService, ProyectoConsultaService>();
builder.Services.AddScoped<IProyectoFlujoService, ProyectoFlujoService>();
builder.Services.AddScoped<IProyectoPasoConsultaService, ProyectoPasoConsultaService>();
builder.Services.AddScoped<IAprobacionDecisionService, AprobacionDecisionService>();
builder.Services.AddScoped<IAprobacionFiltradoService, AprobacionFiltradoService>();
builder.Services.AddScoped<IAprobacionConsultaService, AprobacionConsultaService>();
builder.Services.AddScoped<IProyectoActualizacionService, ProyectoActualizacionService>();
builder.Services.AddScoped<IProyectoValidacionService, ProyectoValidacionService>();
builder.Services.AddScoped<IStatusConsultaServices, StatusConsultaServices>();


// Configurar controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("PermitirTodo"); // Aplicar la política

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Manejo global de excepciones (recomendado para OpenAPI)
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var error = new ApiError
        {
            message = "Ocurrió un error inesperado en el servidor."
        };

        await context.Response.WriteAsJsonAsync(error);
    });
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();