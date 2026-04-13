
using Clinica_TFI.Application;
using Clinica_TFI.Application.Connected_Services;
using Clinica_TFI.Domain.Contracts;
using Clinica_TFI.Infraestructure;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace Clinica_TFI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<ClinicaContext>();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API de Historia Clínica",
                    Version = "v1",
                    Description = "Esta API permite gestionar la historia clínica de pacientes, proporcionando funcionalidades para registrar y listar evoluciones médicas. Cada evolución puede incluir datos estructurados mediante plantillas predefinidas, así como texto libre para observaciones adicionales. Además, es posible asociar recetas digitales y solicitudes de laboratorio a las evoluciones."
                });

                // Añadir información de seguridad para JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Nota: Esta API requiere autenticación mediante un token JWT (JSON Web Token). El token debe enviarse en el encabezado 'Authorization' con el formato: 'Bearer {token}'."
                });


                c.EnableAnnotations();
                c.ExampleFilters();
            });

            builder.Services.AddScoped<IClinicaRepository, ClinicaRepository>();
            builder.Services.AddScoped<ClinicaService>();
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<ExternalAPIService>();
            builder.Services.AddScoped<PrescripcionService>();

            builder.Services.AddAutoMapper(typeof(Program));

            //Dependecy Injection - Fluent Validation
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("IngenieraSoftware-TFI-Clinica2024."))
                };

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
