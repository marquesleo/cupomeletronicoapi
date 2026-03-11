using System.Reflection;
using CupomEletronicoAPI;
using CupomEletronicoAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.WebConfig();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.ConfigureJWT();
builder.Services.ConfigureDependences(Configuration);
builder.Services.ConfigureAutoMapper();

builder.Services.AddCors(options =>
{
    options.AddPolicy("LiberadoGeral", policy =>
    {
        policy
            .SetIsOriginAllowed(_ => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", 
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30)
    .CreateLogger();


builder.Host.UseSerilog();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly(),
                           typeof(Dominio.Queries.OperacaoQuery).Assembly,
                            typeof(Dominio.Commands.SalvarOperacaoCommand).Assembly);


builder.Services.Init(Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlerMiddleware>();
//custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.UseRouting();
app.UseCors("LiberadoGeral");
app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseAuthentication();//parte do JWT
app.UseAuthorization();
app.UseSerilogRequestLogging();

var options = new ForwardedHeadersOptions
{
    ForwardedHeaders = 
        ForwardedHeaders.XForwardedFor |
        ForwardedHeaders.XForwardedProto
};

options.KnownNetworks.Clear();
options.KnownProxies.Clear();

app.UseForwardedHeaders(options);


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

