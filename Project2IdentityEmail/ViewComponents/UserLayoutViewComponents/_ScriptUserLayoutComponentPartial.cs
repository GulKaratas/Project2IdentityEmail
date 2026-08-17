using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.ViewComponents.UserLayoutViewComponents
{
    public class _ScriptUserLayoutComponentPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
