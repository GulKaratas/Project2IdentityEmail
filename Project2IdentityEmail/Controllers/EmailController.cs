using Microsoft.AspNetCore.Mvc;
using Project2IdentityEmail.Dtos;
using Project2IdentityEmail.Services;

namespace Project2IdentityEmail.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailSenderService _emailSender;

        public EmailController(IEmailSenderService emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(MailRequestDto mailRequestDto)
        {
            await _emailSender.SendAsync(
                mailRequestDto.RecieverEmail,
                mailRequestDto.Subject,
                mailRequestDto.MessageDetail);

            return RedirectToAction("Sendbox");
        }
    }
}
