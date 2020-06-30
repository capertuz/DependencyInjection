using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TennisBookings.Web.Domain.Rules;

namespace TennisBookings.Web.Core.DependencyInjection
{
    public static class BookingRulesServiceCollectionExtensions
    {
        public static IServiceCollection AddBookingRules(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Singleton<ICourtBookingRule, ClubIsOpenRule>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<ICourtBookingRule, MaxBookingLengthRule>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<ICourtBookingRule, MaxPeakTimeBookingLengthRule>());
            services.TryAddEnumerable(ServiceDescriptor.Scoped<ICourtBookingRule, MemberCourtBookingsMaxHoursPerDayRule>());
            services.TryAddEnumerable(ServiceDescriptor.Scoped<ICourtBookingRule, MemberBookingsMustNotOverlapRule>());

            services.TryAddScoped<IBookingRuleProcessor, BookingRuleProcessor>();

            return services;
        }
    }
}
