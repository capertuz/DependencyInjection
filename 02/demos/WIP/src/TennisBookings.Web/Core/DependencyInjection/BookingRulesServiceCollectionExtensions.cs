using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TennisBookings.Web.Domain.Rules;

namespace TennisBookings.Web.Core.DependencyInjection
{
    public static class BookingRulesServiceCollectionExtensions
    {
        public static IServiceCollection AddBookingRules(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<ICourtBookingRule>()
                .AddClasses(c => c.AssignableTo<ICourtBookingRule>())
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.TryAddScoped<IBookingRuleProcessor, BookingRuleProcessor>();

            return services;
        }
    }
}