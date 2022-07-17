using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShortStoryNetwork.Data;
using ShortStoryNetwork.Repository;
using ShortStoryNetwork.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortStoryNetwork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host =  Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<Context>(options =>
                options.UseSqlServer(context.Configuration.GetConnectionString("ConnectionString")));

                services.AddScoped<IUserInfosRepository, UserInfosRepository>();
                services.AddScoped<IPostRepository, PostRepository>();
                services.AddScoped<IStatVowelRepository, StatVowelRepository>();

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Short Story Network API",
                        Version = "v1",
                        Description = "Swagger UI for the posting stories",
                    });
                });
            })
            .ConfigureLogging((context, builder) =>
            {
                var logger = LoggerFactory.Create(config =>
                {
                    config.AddConsole();
                }).CreateLogger("Program");
            });

            return host;
        }
    }
}
