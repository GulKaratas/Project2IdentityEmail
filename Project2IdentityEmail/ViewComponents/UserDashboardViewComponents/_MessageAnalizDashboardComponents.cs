using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2IdentityEmail.Context;
using Project2IdentityEmail.Entities;
using Project2IdentityEmail.Services;
using Project2IdentityEmail.ViewModels;


namespace Project2IdentityEmail.ViewComponents.UserDashboardViewComponents
{
    public class _MessageAnalizDashboardComponents : ViewComponent
    {
        private readonly EmailContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IGeminiAnalysisService _geminiService;

        public _MessageAnalizDashboardComponents(
            EmailContext context,
            UserManager<AppUser> userManager,
            IGeminiAnalysisService geminiService)
        {
            _context = context;
            _userManager = userManager;
            _geminiService = geminiService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager
                .FindByNameAsync(User.Identity?.Name);

            if (user == null)
            {
                return View();
            }

            var weekStart = DateTime.Now.Date.AddDays(
                -(int)DateTime.Now.DayOfWeek + 1);

            var messages = await _context.Messages
                .Where(x =>
                    (x.SenderEmail == user.Email ||
                     x.ReceiverEmail == user.Email) &&
                    x.SendDate >= weekStart)
                .OrderByDescending(x => x.SendDate)
                .ToListAsync();

            if (!messages.Any())
            {
                return View(new MessageAnalysisViewModel
                {
                    Summary = "Bu hafta analiz edilecek mesaj bulunmuyor.",
                    MainTopic = "Henüz veri yok",
                    Activity = "Sakin",
                    Tone = "Belirsiz"
                });
            }

            var analysis =
                await _geminiService.AnalyzeMessagesAsync(messages);

            return View(analysis);
        }
    }
}