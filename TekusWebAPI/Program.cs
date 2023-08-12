using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using TekusCore.Application;
using TekusCore.Application.BLL;
using TekusCore.Application.Interfaces.BLL;
using TekusCore.Application.Interfaces.Infrastructure;
using TekusCore.Application.Interfaces.Repositories;
using TekusCore.Infrastructure.Helpers;
using TekusCore.Infrastructure.Repositories;

namespace TekusWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //automapper, fluent validator, mediators
            //auto discovery on dll assembly
            builder.Services.AddTekusApplication();

            //helpers
            builder.Services.AddScoped<IDatabaseHelper, SqlDatabaseHelper>();

            //repositories
            builder.Services.AddScoped<IProviderRepository, ProviderRepository>();

            //bll
            builder.Services.AddScoped<IProviderManager,ProviderManager>();




            builder.Services
                   .AddHttpContextAccessor()
                   .AddAuthorization()
                   .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidateLifetime = true,
                           ValidateIssuerSigningKey = true,
                           ValidIssuer = builder.Configuration["Jwt:Issuer"],
                           ValidAudience = builder.Configuration["Jwt:Audience"],
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                       };
                   });

            builder.Services.AddRequiredScopeAuthorization();





            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
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
                      });
            });

            var app = builder.Build();

            //configure reversehash with property injection
            IConfiguration? config= app.Services.GetService<IConfiguration>();
            TekusCore.Application.BLL.ReverseHash.SetSalt(config!["HashIdsSalt"]!);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}