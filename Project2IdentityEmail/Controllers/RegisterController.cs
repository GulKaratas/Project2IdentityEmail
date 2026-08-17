using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2IdentityEmail.Context;
using Project2IdentityEmail.Dtos;
using Project2IdentityEmail.Entities;
using Project2IdentityEmail.Services;

namespace Project2IdentityEmail.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailContext _emailContext;
        private readonly IEmailSenderService _emailSender;

        public RegisterController(
            UserManager<AppUser> userManager,
            EmailContext emailContext,
            IEmailSenderService emailSender)
        {
            _userManager = userManager;
            _emailContext = emailContext;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRegisterDto createUserRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createUserRegisterDto);
            }

            if (string.IsNullOrWhiteSpace(createUserRegisterDto.Password))
            {
                ModelState.AddModelError(nameof(createUserRegisterDto.Password), "Şifre zorunludur.");
                return View(createUserRegisterDto);
            }

            Random
                random = new Random();
            int code = random.Next(100000,1000000);
            AppUser appUser = new AppUser
            {
                Name = createUserRegisterDto.Name,
                Surname = createUserRegisterDto.Surname,
                UserName = createUserRegisterDto.Username,
                Email = createUserRegisterDto.Email,
                ConfirmCode = code.ToString()   
            };

            var result = await _userManager.CreateAsync(appUser, createUserRegisterDto.Password);
            if (result.Succeeded)
            {
                await _emailSender.SendAsync(
                    createUserRegisterDto.Email,
                    "Hesap Aktifleştirme",
                    "Hesabınızı aktifleştirmek için gerekli olan aktivasyon kodunuz: " + code);

                return RedirectToAction("UserActivation", "Activation", new { email = createUserRegisterDto.Email });
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(createUserRegisterDto);
        }

    }
}