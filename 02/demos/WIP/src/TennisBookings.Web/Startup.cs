using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using TennisBookings.Web.Domain.Rules;
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
            services.AddTransient<IWeatherForecaster, AmazingWeatherForecaster>();
            services.Replace(ServiceDescriptor.Transient<IWeatherForecaster, WeatherForecaster>());
            //services.RemoveAll<IWeatherForecaster>();

            services.TryAddScoped<ICourtBookingService, CourtBookingService>();

            services.TryAddEnumerable(ServiceDescriptor.Singleton<ICourtBookingRule, ClubIsOpenRule>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<ICourtBookingRule, MaxBookingLengthRule>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<ICourtBookingRule, MaxPeakTimeBookingLengthRule>());
            services.TryAddEnumerable(ServiceDescriptor.Scoped<ICourtBookingRule, MemberCourtBookingsMaxHoursPerDayRule>());
            services.TryAddEnumerable(ServiceDescriptor.Scoped<ICourtBookingRule, MemberBookingsMustNotOverlapRule>());
            services.TryAddEnumerable(ServiceDescriptor.Scoped<ICourtBookingRule, MemberBookingsMustNotOverlapRule>());

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            app.UseMvc();
        }
    }
}
