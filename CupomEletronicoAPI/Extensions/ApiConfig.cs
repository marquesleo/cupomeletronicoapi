﻿using Microsoft.AspNetCore.Mvc;

namespace CupomEletronicoAPI.Extensions
{
    public static class ApiConfig
    {

        public static void WebConfig(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true; //seta o valor indicado dentro da api e substitui nas rotas

            });
        }
    }
}
