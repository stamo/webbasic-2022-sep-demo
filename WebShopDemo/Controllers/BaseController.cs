using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebShopDemo.Core.Constants;

namespace WebShopDemo.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public string UserFirstName 
        {
            get 
            {
                string firstName = string.Empty;

                if (User?.Identity?.IsAuthenticated ?? false && User.HasClaim(c => c.Type == ClaimTypeConstants.FirsName))
                {
                    firstName = User.Claims
                        .FirstOrDefault(c => c.Type == ClaimTypeConstants.FirsName)
                        ?.Value ?? firstName;
                }

                return firstName;
            } 
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                ViewBag.UserFirstName = UserFirstName;
            }

            base.OnActionExecuted(context);
        }
    }
}
