using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using SimpleFormsService.Services;
using SimpleFormsService.Services.Abstractions;
using SimpleFormsService.Persistence.Repositories;
using SimpleFormsService.Domain.Repositories;
using SimpleFormsService.Persistence;
using Microsoft.EntityFrameworkCore;
using SimpleFormsService.Configuration;
using Minio.AspNetCore;
using Notify.Client;

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

            Console.WriteLine("==== INFO: GC Notify BaseURL null? " + OpenshiftConfig.GCNotify_BaseURL + " ====");
            Console.WriteLine("==== INFO: GC Notify Admin Template Id null? " + string.IsNullOrWhiteSpace(OpenshiftConfig.GCNotify_Admin_TemplateId) + " ====");
            Console.WriteLine("==== INFO: GC Notify User Template Id null? " + string.IsNullOrWhiteSpace(OpenshiftConfig.GCNotify_User_TemplateId) + " ====");
            Console.WriteLine("==== INFO: GC Notify API Key null? " + string.IsNullOrWhiteSpace(OpenshiftConfig.GCNotify_ApiKey) + " ====");
            Console.WriteLine("==== INFO: Minio endpoint null? " + OpenshiftConfig.MINIO_EndPoint + " ====");
            Console.WriteLine("==== INFO: Minio access key null? " + string.IsNullOrWhiteSpace(OpenshiftConfig.MINIO_AccessKey) + " ====");
            Console.WriteLine("==== INFO: Minio secret key? " + string.IsNullOrWhiteSpace(OpenshiftConfig.MINIO_SecretKey) + " ====");

            services.AddMinio(options =>
            {
                options.Endpoint = OpenshiftConfig.MINIO_EndPoint;
                options.AccessKey = OpenshiftConfig.MINIO_AccessKey;
                options.SecretKey = OpenshiftConfig.MINIO_SecretKey;
            });

            services.AddHttpContextAccessor();

            services.Scan(scan => scan.FromAssembliesOf(typeof(IRepositoryBase<>), typeof(RepositoryBase<>))
                .AddClasses(classes => classes.AssignableTo(typeof(IRepositoryBase<>)).Where(type => !type.IsGenericType), false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Scan(scan => scan.FromAssembliesOf(typeof(IServiceBase), typeof(ServiceBase))
                .AddClasses(classes => classes.AssignableTo<IServiceBase>(), false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddSingleton(x => new NotificationClient(OpenshiftConfig.GCNotify_BaseURL, OpenshiftConfig.GCNotify_ApiKey));

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
