using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using rift.data;
using rift.domain;
using rift.interfaces.Repository;
using rift.services.Repository;
using rift.Web.Configuration;

namespace rift.web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AcervoContext>(options => {
                options.UseInMemoryDatabase("dbacervo");
                options.UseLoggerFactory(LoggerFactory.Create(builder =>
                {
                    builder.AddDebug();
                    builder.AddConsole();
                }));
                });

            services.AddScoped<IRepository<Person>, Repository<Person>>();
            services.AddScoped<IRepository<Company>, Repository<Company>>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers();

            services.AddHealthChecks();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "rift.web", Version = "v1" });
            });

            services.AddProblemDetailsModule(Environment);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "rift.web v1"));
            }

            app.UseRouting();
            app.UseHealthChecks("/health");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseApplicationProblemDetails();

        }
    }
}
