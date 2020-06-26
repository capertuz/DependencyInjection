using TennisBookings.Web.Domain;

namespace TennisBookings.Web.Services
{
    public interface IMembershipAdvertBuilder
    {
        MembershipAdvertBuilder WithDiscount(decimal discount);
        MembershipAdvert Build();
    }
}