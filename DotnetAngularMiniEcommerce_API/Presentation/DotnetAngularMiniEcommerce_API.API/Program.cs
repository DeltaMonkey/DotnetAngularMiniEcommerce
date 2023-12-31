using DotnetAngularMiniEcommerce_API.Application.Validators.Products;
using DotnetAngularMiniEcommerce_API.Application.ViewModels.Products;
using DotnetAngularMiniEcommerce_API.Infrastructure;
using DotnetAngularMiniEcommerce_API.Infrastructure.Filters;
using DotnetAngularMiniEcommerce_API.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using System;

namespace DotnetAngularMiniEcommerce_API.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddPersistanceServices();
            builder.Services.AddInfrastructureService();

            builder.Services.AddCors(options => options.AddDefaultPolicy(policy => {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(Configuration.CorsUrlList.ToArray());
            }));

            builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
                .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}