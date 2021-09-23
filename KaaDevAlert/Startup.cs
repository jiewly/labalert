using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using KaaDevAlert.Job;
using KaaDevAlert.Models;
using KaaDevAlert.Repository;
using KaaDevAlert.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace KaaDevAlert
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer container { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.


        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Add Connection Hangfire DB 
            services.AddHangfire(config => { config.UseSqlServerStorage("Server=(localdb)\\MSSQLLocalDB;Database=KaaDevAlert;User Id=sa;Password=P@ssw0rd"); });
            //Add Configurations
            services.Configure<Configuration>(Configuration.GetSection("Configuration"));
            //Add Massage
            services.Configure<Massage>(Configuration.GetSection("Massage"));
            services.AddHangfireServer();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpencificOrigins", builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddControllersWithViews();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "V1" });

            });

            //Now register our services with Autofac container
            var builder = new ContainerBuilder();

            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess);
            //Register your Repository.

            //Register your Service.
            builder.RegisterAssemblyTypes(typeof(Massage).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .SingleInstance();


            //Register your Repository.
            var repositoryAssembly = typeof(MassageRepository).Assembly;
            builder.RegisterAssemblyTypes(repositoryAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .SingleInstance();


            //container = builder.Build();
            builder.Populate(services);
            this.container = builder.Build();

            return new AutofacServiceProvider(this.container);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobs)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("AllowSpencificOrigins");
            app.UseRouting();
            app.UseHangfireDashboard();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");


            });

            app.UseAuthorization();
            backgroundJobs.Enqueue(() => Console.WriteLine("Job  start!"));
            var jobId = BackgroundJob.Schedule(() => Console.WriteLine("Delayed!"), TimeSpan.FromDays(7));
            var time = container.Resolve<Daily>().GetTimeAlert();

            var hour = Convert.ToInt32(time["Hour"]);
            var minute = Convert.ToInt32(time["Minute"]);
            //var minute = Convert.ToInt32(Configuration?.GetSection("DateTimeJobSchedule:Minute").Value);
            RecurringJob.AddOrUpdate(() => container.Resolve<Daily>().Run(), Cron.Daily(hour, minute), TimeZoneInfo.Local); // Daily, Minutely, Hourly, Weekly

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
