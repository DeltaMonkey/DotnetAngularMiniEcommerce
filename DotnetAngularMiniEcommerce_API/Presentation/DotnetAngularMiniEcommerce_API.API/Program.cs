using DotnetAngularMiniEcommerce_API.Application;
using DotnetAngularMiniEcommerce_API.Application.Validators.Products;
using DotnetAngularMiniEcommerce_API.Infrastructure;
using DotnetAngularMiniEcommerce_API.Infrastructure.Enums;
using DotnetAngularMiniEcommerce_API.Infrastructure.Filters;
using DotnetAngularMiniEcommerce_API.Infrastructure.Services.Storage.Azure;
using DotnetAngularMiniEcommerce_API.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DotnetAngularMiniEcommerce_API.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddPersistanceServices();
            builder.Services.AddInfrastructureService();
            builder.Services.AddApplicationService();

            //builder.Services.AddStorage(StorageType.Local);
            builder.Services.AddStorage<AzureStorage>();

            builder.Services.AddCors(options => options.AddDefaultPolicy(policy => {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(Configuration.CorsUrlList.ToArray());
            }));

            builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
                .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Admin", options => {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true, // Olusuturulacak token degerini kimlerin/hangi originlerin/sitelerin kullanacagini belirttigimiz degerdir www.bilememne.com
                    ValidateIssuer = true, // Olusturulacak token degerini kimin dagittigini ifade edecegimiz alandir. www.exampleapi.com
                    ValidateLifetime = true, // Olusturulan token degerinin suresini kontrol edecek olan dogrulamadir
                    ValidateIssuerSigningKey = true, // Uretilecek token degerinin uygulamamiza ait bir deger oldugunu ifade eden security key verisinin dogrulamasidir

                    ValidAudience = builder.Configuration["Token:Audience"],
                    ValidIssuer = builder.Configuration["Token:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
                };
            });

            builder.Services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseCors();

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}