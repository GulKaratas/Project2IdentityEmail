using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2IdentityEmail.Context;
using Project2IdentityEmail.Entities;
using Project2IdentityEmail.Models;

namespace Project2IdentityEmail.ViewComponents.UserDashboardViewComponents
{
    public class _MessageInfoDashboardComponents : ViewComponent
    {
        private readonly EmailContext _context;
        private readonly UserManager<AppUser> _userManager;

        public _MessageInfoDashboardComponents(
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
                return View(new MessageInfoDashboardViewModel());
            }

            var receivedMessages = _context.Messages
                .Where(x => x.ReceiverEmail == user.Email);

            var sentMessages = _context.Messages
                .Where(x => x.SenderEmail == user.Email);

            var model = new MessageInfoDashboardViewModel
            {
                ReceivedCount = await receivedMessages.CountAsync(),

                SentCount = await sentMessages.CountAsync(),

                LastReceivedDate = await receivedMessages
                    .OrderByDescending(x => x.SendDate)
                    .Select(x => (DateTime?)x.SendDate)
                    .FirstOrDefaultAsync(),

                LastSentDate = await sentMessages
                    .OrderByDescending(x => x.SendDate)
                    .Select(x => (DateTime?)x.SendDate)
                    .FirstOrDefaultAsync()
            };

            return View(model);
        }
    }
}