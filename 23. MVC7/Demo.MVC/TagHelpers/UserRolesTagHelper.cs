using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Demo.MVC.TagHelpers
{
    // <user-role></user-role>
    // <td i-user></td>

    [HtmlTargetElement("td", Attributes = "i-user")]
    public class UserRolesTagHelper : TagHelper
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRolesTagHelper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HtmlAttributeName("i-user")]
        public string? UserId { get; set; }


        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> roles = [];

            if(!string.IsNullOrWhiteSpace(UserId))
            {
                AppUser? user = await _userManager.FindByIdAsync(UserId);
                if(user is not null)
                {
                    roles = (await _userManager.GetRolesAsync(user)).ToList();
                }
            }

            output.Content.SetContent(roles.Count == 0 ? "No Roles" : string.Join(", ", roles));
        }
    }
}
