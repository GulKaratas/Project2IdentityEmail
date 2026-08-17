using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2IdentityEmail.Dtos;
using Project2IdentityEmail.Entities;
using Project2IdentityEmail.ViewModels;

namespace Project2IdentityEmail.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private const long MaxImageSizeInBytes = 4 * 1024 * 1024;

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _environment;

        public ProfileController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return NotFound();
            }

            return View(BuildViewModel(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserProfile([Bind(Prefix = "Profile")] UserEditDto profile)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return NotFound();
            }

            ValidateImage(profile.Image);

            if (!ModelState.IsValid)
            {
                return View(BuildViewModel(user, profile));
            }

            user.Name = profile.Name;
            user.Surname = profile.Surname;
            user.Email = profile.Email;
            user.PhoneNumber = profile.Phone;

            if (profile.Image is { Length: > 0 })
            {
                user.ImageUrl = await SaveProfileImageAsync(profile.Image);
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["ProfileSuccess"] = "Profil bilgileriniz güncellendi.";
                return RedirectToAction(nameof(UserProfile));
            }

            var model = BuildViewModel(user, profile);
            model.ProfileErrors.AddRange(result.Errors.Select(error => error.Description));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword([Bind(Prefix = "Password")] ChangePasswordDto password)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user is null)
            {
                return NotFound();
            }

            var model = BuildViewModel(user);

            if (!ModelState.IsValid)
            {
                return View(nameof(UserProfile), model);
            }

            var result = await _userManager.ChangePasswordAsync(user, password.CurrentPassword, password.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["PasswordSuccess"] = "Şifreniz başarıyla güncellendi.";
                return RedirectToAction(nameof(UserProfile));
            }

            model.PasswordErrors.AddRange(result.Errors.Select(error => error.Description));
            return View(nameof(UserProfile), model);
        }

        private ProfileViewModel BuildViewModel(AppUser user, UserEditDto profile = null)
        {
            profile ??= new UserEditDto
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.PhoneNumber
            };

            profile.ImageUrl = user.ImageUrl;

            return new ProfileViewModel
            {
                Profile = profile,
                UserName = user.UserName,
                EmailConfirmed = user.EmailConfirmed,
                IsLockedOut = user.LockoutEnd.HasValue && user.LockoutEnd > DateTimeOffset.UtcNow
            };
        }

        private void ValidateImage(IFormFile image)
        {
            if (image is null || image.Length == 0)
            {
                return;
            }

            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

            if (!AllowedImageExtensions.Contains(extension))
            {
                ModelState.AddModelError("Profile.Image", "Yalnızca JPG, PNG, GIF veya WEBP dosyaları yükleyebilirsiniz.");
            }

            if (image.Length > MaxImageSizeInBytes)
            {
                ModelState.AddModelError("Profile.Image", "Profil fotoğrafı en fazla 4 MB olabilir.");
            }
        }

        private async Task<string> SaveProfileImageAsync(IFormFile image)
        {
            var folder = Path.Combine(_environment.WebRootPath, "images");
            Directory.CreateDirectory(folder);

            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName).ToLowerInvariant();

            await using var stream = System.IO.File.Create(Path.Combine(folder, fileName));
            await image.CopyToAsync(stream);

            return fileName;
        }
    }
}
