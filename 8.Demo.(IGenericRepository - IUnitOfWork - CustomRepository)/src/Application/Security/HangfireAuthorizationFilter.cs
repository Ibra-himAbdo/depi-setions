using Hangfire.Dashboard;

namespace Application;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        return context.GetHttpContext().User.IsInRole(ApplicationRoles.Admin);
    }
}
