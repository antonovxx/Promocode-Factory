using Pcf.Dictionary.DataAccess.Extensions.Redis;

namespace Pcf.Dictionary.WebHost;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddRedis(builder.Configuration.GetConnectionString("Redis"));

        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "PromoCode Factory Dictionary API Doc",
                Version = "v1"
            });
        });

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }
        
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.MapControllers();

        app.Run();
    }
}