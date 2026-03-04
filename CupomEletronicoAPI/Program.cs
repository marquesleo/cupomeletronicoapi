using System.Reflection;
using CupomEletronicoAPI;
using CupomEletronicoAPI.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;

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



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

