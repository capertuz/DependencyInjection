using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Core.DependencyInjection;
using TennisBookings.Web.Core.Middleware;
using TennisBookings.Web.Data;
using TennisBookings.Web.Services;

namespace TennisBookings.Web
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
            services.Configure<ExternalServicesConfig>(Configuration.GetSection("ExternalServices"));
            services.Configure<FeaturesConfiguration>(Configuration.GetSection("Features"));

            services.AddWeatherForecasting();

            services.TryAddScoped<ICourtBookingService, CourtBookingService>();

            services.AddBookingRules();

            services.TryAddScoped<IBookingConfiguration>(sp => sp.GetService<IOptions<BookingConfiguration>>().Value);

            services.AddNotifications();

            services.AddMembershipServices().AddGreetings().AddCaching();//you can concatenate multiple extensions

            //services.AddCourtServices(); -replaced with Autofac registration in ConfigureContainer

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddIdentity<TennisBookingsUser, TennisBookingsRole>()
                .AddEntityFrameworkStores<TennisBookingDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddDbContext<TennisBookingDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseLastRequestTracking(); // only track requests which make it to MVC, i.e. not static files

            app.UseMvc();
        }

        // This method is called after ConfigureServices
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<CourtMaintenanceService>()
                .As<ICourtMaintenanceService>()
                .InstancePerLifetimeScope();
        }
    }
}
