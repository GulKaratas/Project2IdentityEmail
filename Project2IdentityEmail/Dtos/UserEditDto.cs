using System.ComponentModel.DataAnnotations;

namespace Project2IdentityEmail.Dtos
{
    public class UserEditDto
    {
        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
        public string Name { get; set; }

        [Display(Name = "Soyad")]
        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
        public string Surname { get; set; }

        [Display(Name = "E-posta")]
        [Required(ErrorMessage = "E-posta alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string Email { get; set; }

        [Display(Name = "Telefon")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası girin.")]
        public string? Phone { get; set; }

        [Display(Name = "Profil fotoğrafı")]
        public IFormFile? Image { get; set; }

        public string? ImageUrl { get; set; }
    }
}
