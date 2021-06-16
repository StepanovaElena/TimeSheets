﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Timesheets.Models.Dto.Authentication;
using TimeSheets.Data.EntityConfiguration;
using TimeSheets.Data.Implementation;
using TimeSheets.Data.Interfaces;
using TimeSheets.Domain.Implementation;
using TimeSheets.Domain.Interfaces;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using TimeSheets.Models.Dto;
using TimeSheets.Infrastructure.Validation;
using FluentValidation;
using TimeSheets.Models.Dto.Requests;

namespace TimeSheets.Infrastructure.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<TimeSheetDbContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("Postgres"),
                    b=>b.MigrationsAssembly("TimeSheets"));
            });
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtAccessOptions>(configuration.GetSection("Authentication:JwtAccessOptions"));

            var jwtSettings = new JwtOptions();
            configuration.Bind("Authentication:JwtAccessOptions", jwtSettings);

            services.AddTransient<ILoginManager, LoginManager>();

            services
                .AddAuthentication(
                    x =>
                    {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = jwtSettings.GetTokenValidationParameters();
                });
        }

        public static void ConfigureDomainManagers(this IServiceCollection services)
        {    
            services.AddScoped<IClientManager, ClientManager>();
            services.AddScoped<IContractManager, ContractManager>();
            services.AddScoped<IEmployeeManager, EmployeeManager>();
            services.AddScoped<IInvoiceManager, InvoiceManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ISheetManager, SheetManager>();
            services.AddScoped<IUserManager, UserManager>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {           
            services.AddScoped<IClientRepo, ClientRepo>();
            services.AddScoped<IContractRepo, ContractRepo>();
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            services.AddScoped<IInvoiceRepo, InvoiceRepo>();
            services.AddScoped<IServiceRepo, ServiceRepo>();
            services.AddScoped<ISheetRepo, SheetRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
        }
        
        public static void ConfigureBackendSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Timesheets", Version = "v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference(){Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
        public static void ConfigureValidtion(this IServiceCollection services)
        {            
            services.AddTransient<IValidator<SheetRequest>, SheetRequestValidator>();
            services.AddTransient<IValidator<InvoiceRequest>, InvoiceRequestValidator>();
            services.AddTransient<IValidator<UserRequest>, UserRequestValidator>();
            services.AddTransient<IValidator<EmployeeRequest>, EmployeeRequestValidator>();
            services.AddTransient<IValidator<ClientRequest>, ClientRequestValidator>();
        }
    }
}