using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace WebApi.Extentions
{
    public static class WebAppExtentions
    {
        public static async Task AddWebAppExtention(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PORTIFY.UZ API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var result = JsonSerializer.Serialize(new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(e => new
                        {
                            name = e.Key,
                            status = e.Value.Status.ToString(),
                            description = e.Value.Description
                        }),
                        totalDuration = report.TotalDuration.TotalSeconds
                    });

                    await context.Response.WriteAsync(result);
                }
            });

            app.UseCors("AddCors");

            app.MapControllers();

            await app.UpdateMigration();
            await app.Seed();
        }

        private static async Task UpdateMigration(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.MigrateAsync();
        }

        private static async Task Seed(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var hashservice = scope.ServiceProvider.GetRequiredService<IHashService>();
            await dbContext.SeedAsync(hashservice);
        }
    }
}
