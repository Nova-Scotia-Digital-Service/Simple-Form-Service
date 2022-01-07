using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleFormsService.Web.Public.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleFormsService.Services;
using SimpleFormsService.Services.Abstractions;
using SimpleFormsService.Persistence.Repositories;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Persistence;
using Microsoft.EntityFrameworkCore;
using SimpleFormsService.Configuration;
using Minio.AspNetCore;

namespace SimpleFormsService.Web.Public
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SimpleFormsServiceDbContext>(options => options.UseNpgsql(OpenshiftConfig.Postgres_ConnectionString));

            services.AddMinio(options =>
            {
                options.Endpoint = OpenshiftConfig.MINIO_EndPoint;
                options.AccessKey = OpenshiftConfig.MINIO_AccessKey;
                options.SecretKey = OpenshiftConfig.MINIO_SecretKey;
            });

            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            services.AddRazorPages()
                .AddRazorPagesOptions(options => {
                    options.RootDirectory = "/Forms";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
