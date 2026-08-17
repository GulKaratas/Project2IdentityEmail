using System.ComponentModel.DataAnnotations;

namespace Project2IdentityEmail.Dtos
{
    public class ChangePasswordDto
    {
        [Display(Name = "Mevcut Şifre")]
        [Required(ErrorMessage = "Mevcut şifrenizi girin.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "Yeni Şifre")]
        [Required(ErrorMessage = "Yeni şifrenizi girin.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Yeni Şifre (Tekrar)")]
        [Required(ErrorMessage = "Yeni şifrenizi tekrar girin.")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Şifreler birbiriyle eşleşmiyor.")]
        public string ConfirmNewPassword { get; set; }
    }
}
