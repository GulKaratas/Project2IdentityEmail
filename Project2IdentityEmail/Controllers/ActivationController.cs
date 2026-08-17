using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2IdentityEmail.Context;
using Project2IdentityEmail.Dtos;
using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.Controllers
{
    public class ActivationController : Controller
    {
       private readonly UserManager<AppUser> _userManager;
        private readonly EmailContext _emailContext;

        public ActivationController(EmailContext emailContext, UserManager<AppUser> userManager)
        {
            _emailContext = emailContext;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult UserActivation(string email)
        {
            return View(new ConfirmCodeDto { Email = email });
        }

        [HttpPost]
        public async Task<IActionResult> UserActivation(ConfirmCodeDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(model);
            }

            var enteredCode = model.confirmCode?.Trim();
            var storedCode = user.ConfirmCode?.Trim();

            if (!string.IsNullOrEmpty(storedCode) && enteredCode == storedCode)
            {
                user.EmailConfirmed = true;
                user.TwoFactorEnabled = true;

                await _userManager.UpdateAsync(user);

                return RedirectToAction("UserLogin", "Login");
            }

            ModelState.AddModelError("", "Girdiğiniz aktivasyon kodu hatalı. Lütfen tekrar deneyin.");

            return View(model);
        }
    }
}
