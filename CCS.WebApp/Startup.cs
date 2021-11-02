using CCS.Repository.Entities;
using CCS.Repository.Infrastructure.Contexts;
using CCS.Repository.Infrastructure.Repositories;
using CCS.WebApp.Extensions;
using CCS.WebApp.Services;
using CCS.WebApp.Settings;
using CSS.GPIO.Relays;
using CSS.GPIO.TemperatureSensors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Channels;
using Unosquare.RaspberryIO.Abstractions;
using Microsoft.OpenApi.Models;
using Unosquare.RaspberryIO;

namespace CCS.WebApp
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

            services.AddControllersWithViews();

            services.ConfigurePoco<GpioSettings>(Configuration.GetSection("GpioSettings"));


            services.AddDbContext<StationContext>(options => options.UseSqlite("Data Source=station.db"));

            services.AddScoped<IMeasureRepository, MeasureRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();

            services.AddHostedService<MeasureHostedService>();
            services.AddHostedService<ControlHostedService>();
            services.AddSingleton(x => Channel.CreateUnbounded<Setting>());

#if DEBUG
            services.AddSingleton<ITemperatureSensor>(sp => new TemperatureSensorForTesting(P1.Pin22));
            services.AddSingleton<IGpioRelay, GpioRelayForTesting>();
#else
            services.AddSingleton<ITemperatureSensor>(sp => new TemperatureSensor(Pi.Gpio[22]));
            services.AddSingleton<IGpioRelay, GpioRelay>();
#endif

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApp", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApp v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<StationContext>().Database.Migrate();
            }
        }
    }
}
