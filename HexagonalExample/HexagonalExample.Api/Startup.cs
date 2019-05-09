using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using HexagonalExample.Api.Filters;
using HexagonalExample.Infrastructure.Ioc;

namespace HexagonalExample.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureIoc(Configuration);

            services.AddMvc(options => 
            {
                options.Filters.Add<ValidationExceptionFilter>();
                options.Filters.Add<ExceptionFilter>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(configuration =>
            {
                configuration.SwaggerDoc("v1", new Info { Title = "HexagonalExample Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(x =>
                x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials());

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.RoutePrefix = string.Empty;
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "HexagonalExample Api V1");
            });
        }
    }
}
