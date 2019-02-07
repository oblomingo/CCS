using CCS.Repository.Infrastructure.Contexts;
using CCS.Repository.Infrastructure.Repositories;
using CCS.Web.Services;
using CSS.GPIO;
using CSS.GPIO.Relays;
using CSS.GPIO.TemperatureSensors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Unosquare.RaspberryIO.Gpio;

namespace CCS.Web
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "CCS" }); });
	        services.AddDbContext<StationContext>(options => options.UseSqlite("Data Source=../CCS.Repository/station.db"));

			services.AddScoped<IMeasureRepository, MeasureRepository>();
	        services.AddScoped<ISettingRepository, SettingRepository>();
			services.AddScoped<IGpioManager, GpioManager>();

	        services.AddHostedService<MeasureHostedService>();
	        services.AddHostedService<ControlHostedService>();
	        services.AddSingleton<IBackgroundQueue, ControlHostedServiceQueue>();

			#if DEBUG
				services.AddSingleton<ITemperatureSensor>(sp => new TemperatureSensorForTesting(P1.Gpio22));
				services.AddSingleton<IGpioRelay, GpioRelayForTesting>();
			#else
				services.AddSingleton<ITemperatureSensor>(sp => new TemperatureSensor(P1.Gpio22));
				services.AddSingleton<IGpioRelay, GpioRelay>();
			#endif
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

			app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CCS");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
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
    }
}
