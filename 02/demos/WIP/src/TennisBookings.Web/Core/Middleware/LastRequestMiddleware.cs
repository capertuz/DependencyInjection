using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using TennisBookings.Web.Data;

namespace TennisBookings.Web.Core.Middleware
{
    public class LastRequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserManager<TennisBookingsUser> _userManager;

        public LastRequestMiddleware(RequestDelegate next, UserManager<TennisBookingsUser> userManager)
        {
            _next = next;
            _userManager = userManager;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User?.Identity != null && context.User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(context.User.Identity.Name);

                if (user != null)
                {
                    user.LastRequestUtc = DateTime.UtcNow;

                    await _userManager.UpdateAsync(user);
                }
            }

            await _next(context);
        }
    }
}
