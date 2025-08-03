﻿using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using WarehouseService.Data;
using WarehouseService.Middlewares;
using WarehouseService.Validators;
using WarehouseService.Models;

namespace WarehouseService.Extentions
{
    public static class ServicesExtensions
    {
        public static WebApplicationBuilder AddData(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
                builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseInMemoryDatabase("TestDB"));
            else
                builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            return builder;
        }

        public static WebApplicationBuilder AddControllers(this WebApplicationBuilder builder)
        {
            builder.Services.AddRouting(opt => opt.LowercaseUrls = true);
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            return builder;
        }

        public static WebApplicationBuilder AddFluentValidation(this WebApplicationBuilder builder)
        {
            builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();
            builder.Services.AddFluentValidationAutoValidation(config =>
            {
                config.DisableDataAnnotationsValidation = true;
            });
            return builder;
        }

        public static WebApplicationBuilder AddAutoMapper(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new AutoMapperProfile()));
            return builder;
        }

        public static WebApplicationBuilder AddAppServices(this WebApplicationBuilder builder)
        {
            return builder;
        }

        public static WebApplicationBuilder AddExceptionHandler(this WebApplicationBuilder builder)
        {
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
            return builder;
        }

        public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Warehouse API",
                    Version = "v1"
                });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "WarehouseService.xml"));
            });

            return builder;
        }
    }
}
