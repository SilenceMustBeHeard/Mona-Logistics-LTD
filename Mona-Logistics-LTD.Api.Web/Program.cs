using Microsoft.OpenApi;


namespace Mona_Logistics_LTD.Api.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        // Swagger  instead AddOpenApi
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Mona Logistics API",
                Version = "v1"
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            //  Swagger UI instead MapOpenApi
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mona Logistics API V1");
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}