using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2IdentityEmail.Context;
using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.ViewComponents.UserDashboardViewComponents
{
    public class _RecentEmailsDashboardComponents : ViewComponent
    {
        private readonly EmailContext _context;
        private readonly UserManager<AppUser> _userManager;

        public _RecentEmailsDashboardComponents(
            EmailContext context,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                return View(new List<Message>());
            }

            var messages = _context.Messages
                .Where(x =>
                    x.ReceiverEmail == user.Email ||
                    x.SenderEmail == user.Email)
                .OrderByDescending(x => x.SendDate)
                .Take(5)
                .ToList();

            return View(messages);
        }
    }
}