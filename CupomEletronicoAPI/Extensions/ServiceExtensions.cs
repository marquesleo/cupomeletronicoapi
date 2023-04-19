﻿using Dominio.Models;
using Dominio.Services;
using Dominio.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Text;

namespace CupomEletronicoAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void Init(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(provider => configuration);
        }

        public static void ConfigureJWT(this IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Dominio.Services.Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.SaveToken = true;
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero,
                 };
             })
             .AddCookie("Cookies", options =>
             {
                 options.LoginPath = "/login";
                 options.ExpireTimeSpan = TimeSpan.FromDays(7);
             });
        }


        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerGen(c =>
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using Bearer scheme (Example: 'Bearer 1234abcdef')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            }));
            services.AddSwaggerGen(c =>
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {

                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()

                }
           }));
        }

        public static void ConfigureDependences(this IServiceCollection services,
                                                IConfiguration configuration)
        {
            services.AddSingleton<Dominio.Services.VestilloRotinas.Interface.IFuncionarioService, Dominio.Services.VestilloRotinas.FuncionarioService>();
            services.AddSingleton<IUsuario,UsuarioService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IJwtUtils, JwtUtils>();
           
        }


    }
}
