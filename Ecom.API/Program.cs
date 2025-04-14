using Ecom.Infrastructure;
using System.Text;

namespace Ecom.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.infrastructureConfiguration(builder.Configuration);
            builder.Services.AddMemoryCache();
            builder.Services.AddControllers();

            // Swagger setup
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseHttpsRedirection();
            app.UseRouting();


            // Enable authentication and authorization
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}