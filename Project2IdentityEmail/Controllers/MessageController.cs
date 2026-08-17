using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2IdentityEmail.Context;
using Project2IdentityEmail.Entities;
using Project2IdentityEmail.ViewModels;

namespace Project2IdentityEmail.Controllers
{
    public class MessageController : Controller
    {
        private readonly EmailContext _context;
        private readonly UserManager<AppUser> _userManager;

        public MessageController(EmailContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Inbox()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var messages = _context.Messages
                .Where(x => x.ReceiverEmail == user.Email)
                .OrderByDescending(x => x.SendDate)
                .ToList();

            var categories = _context.Categories
                .Where(x => x.CategoryStatus)
                .ToList();

            var model = new InboxViewModel
            {
                Messages = messages,
                Categories = categories
            };

            return View(model);
        }
        public async Task<IActionResult> Sendbox()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var messages = _context.Messages
                .Where(x => x.SenderEmail == user.Email)
                .OrderByDescending(x => x.SendDate)
                .ToList();

            var model = new SendMessageViewModel
            {
                Messages = messages
            };

            return View(model);
        }


        [HttpGet]

        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(Message message)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            message.SenderEmail = user.Email;
            message.SendDate = DateTime.Now;
            message.IsStatus = false;

            _context.Messages.Add(message);

            await _context.SaveChangesAsync();

            return RedirectToAction("Sendbox");
        }

        public IActionResult MessageDetails(int id)
        {
            var message = _context.Messages
                .FirstOrDefault(x => x.MessageId == id);

            if (message == null)
                return NotFound();

            return View(message);
        }
    }
}