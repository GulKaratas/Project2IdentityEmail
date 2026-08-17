using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2IdentityEmail.Entities;
using Project2IdentityEmail.ViewModels;

public class _ProfileInfoDashboardComponents : ViewComponent
{
    private readonly UserManager<AppUser> _userManager;

    public _ProfileInfoDashboardComponents(
        UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);

        if (user == null)
        {
            return View(new ProfileInfoDashboardViewModel());
        }

        var model = new ProfileInfoDashboardViewModel
        {
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            ImageUrl = user.ImageUrl,
            EmailConfirmed = user.EmailConfirmed
        };

        return View(model);
    }
}