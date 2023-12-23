using DotnetAngularMiniEcommerce_API.Persistence;

namespace DotnetAngularMiniEcommerce_API.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddPersistanceServices();

            builder.Services.AddCors(options => options.AddDefaultPolicy(policy => {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(Configuration.CorsUrlList.ToArray());
            }));

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}