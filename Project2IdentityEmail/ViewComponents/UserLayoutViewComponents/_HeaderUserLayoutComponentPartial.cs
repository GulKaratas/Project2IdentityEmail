using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2IdentityEmail.Context;
using Project2IdentityEmail.Entities;
using Project2IdentityEmail.ViewModels;

namespace Project2IdentityEmail.ViewComponents.UserLayoutViewComponents
{
    public class _HeaderUserLayoutComponentPartial : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailContext _context;

        public _HeaderUserLayoutComponentPartial(
            UserManager<AppUser> userManager,
            EmailContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return View(new HeaderUserViewModel());
            }

            var inbox = _context.Messages
                .Where(x => x.ReceiverEmail == user.Email);

            var model = new HeaderUserViewModel
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                ImageUrl = user.ImageUrl,
                UnreadCount = inbox.Count(x => !x.IsStatus),
                RecentMessages = inbox
                    .OrderByDescending(x => x.SendDate)
                    .Take(5)
                    .ToList()
            };

            return View(model);
        }
    }
}
