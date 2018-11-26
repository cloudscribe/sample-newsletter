using Hangfire.Dashboard;

namespace newsletter.DemoWeb.Config
{
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return httpContext.User.IsInRole("Administrators");
            //return httpContext.User.Identity.IsAuthenticated;
        }
    }
}
