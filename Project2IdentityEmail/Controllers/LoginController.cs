using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2IdentityEmail.Context;
using Project2IdentityEmail.Dtos;
using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly EmailContext _emailContext;

        public LoginController(SignInManager<AppUser> signInManager, EmailContext emailContext)
        {
            _signInManager = signInManager;
            _emailContext = emailContext;
        }

        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(LoginUserDto loginUserDto)
        {
            var value = _emailContext.Users
                .FirstOrDefault(x => x.UserName == loginUserDto.Username);

            if (value == null)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                return View();
            }

            if (!value.EmailConfirmed)
            {
                ModelState.AddModelError("", "Lütfen önce e-posta adresinizi doğrulayın.");
                return View();
            }

            if (!value.TwoFactorEnabled)
            {
                ModelState.AddModelError("", "Lütfen aktivasyon işlemini tamamlayın.");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(
                loginUserDto.Username,
                loginUserDto.Password,
                false,
                false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("UserLogin", "Login");
        }
    }
}