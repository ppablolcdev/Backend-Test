using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using Bsol.Business.Template.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bsol.Business.Template.Infrastructure;

[ExcludeFromCodeCoverage]
public static class StartupSetup
{
    public static void AddDbContext(this IServiceCollection services, string connectionString) =>
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString,
                options => options.MigrationsAssembly(typeof(ConfigureServices).Namespace)))
        .AddScoped<DbConnection>(provider =>
        {
            return new SqlConnection(connectionString);
        });

    //public static void AddDbContext(this IServiceCollection services, string readConnectionString, string writeConnectionString) =>
    //    services.AddDbContext<AppDbContext>(options =>
    //        options.UseSqlServer(writeConnectionString,
    //                options => options.MigrationsAssembly(typeof(ConfigureServices).Namespace)))
    //          .AddScoped<DbConnection>(provider =>
    //          {
    //              return new SqlConnection(readConnectionString);
    //          });
    //public static void AddPostgresDbContext(this IServiceCollection services, string connectionString) =>
    //services.AddDbContext<AppDbContext>(options =>
    //    options.UseNpgsql(connectionString,
    //        options => options.MigrationsAssembly(typeof(ConfigureServices).Namespace)))
    //.AddScoped<DbConnection>(provider =>
    //{
    //    return new SqlConnection(connectionString);
    //});
}
