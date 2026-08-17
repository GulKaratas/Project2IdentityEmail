using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.ViewComponents.UserLayoutViewComponents
{
    public class _SidebarUserLayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
