# 📧 Rose Mail

Rose Mail, **ASP.NET Core Identity** ile geliştirilmiş, kullanıcıların güvenli bir şekilde kayıt olup giriş yapabildiği ve uygulama içerisinde e-posta gönderip alabileceği web tabanlı bir mail uygulamasıdır.

Uygulamada kullanıcı kayıt ve e-posta aktivasyonu, giriş sistemi, gelen/giden kutusu, mesaj detayları, profil yönetimi ve dashboard gibi özelliklerin yanı sıra **Google Gemini ile haftalık mesaj analizi** bulunmaktadır.

Arayüz tarafında **Mendy Admin Template** temel alınarak Rose Mail branding ve özel tasarımlar uygulanmıştır.

---

## 📌 Proje Özeti

Rose Mail ile kullanıcılar;

- Hesap oluşturabilir.
- E-posta üzerinden hesaplarını aktive edebilir.
- Güvenli şekilde giriş yapabilir.
- Gelen mesajlarını görüntüleyebilir.
- Mesaj gönderebilir.
- Gönderilen mesajlarını görüntüleyebilir.
- Mesaj detaylarını inceleyebilir.
- Mesajlarını kategorilere ayırabilir.
- Profil bilgilerini güncelleyebilir.
- Profil fotoğrafını değiştirebilir.
- Şifresini değiştirebilir.
- Dashboard üzerinden mail istatistiklerini görüntüleyebilir.
- Google Gemini ile mesaj aktivitelerinin haftalık analizini görebilir.

---

# 🚀 Özellikler

### 🔐 Kullanıcı Yönetimi

- ASP.NET Core Identity ile kullanıcı yönetimi
- Kullanıcı kayıt sistemi
- Kullanıcı giriş ve çıkış işlemleri
- E-posta aktivasyonu
- 6 haneli aktivasyon kodu
- Onaylanmamış hesapların girişinin engellenmesi
- Şifre değiştirme
- Türkçe Identity hata mesajları
- `CustomIdentityValidator`

### 📧 Mail Sistemi

- Gelen kutusu
- Gönderilen mesajlar
- Yeni mesaj oluşturma
- Mesaj detayları
- Mesaj kategorileri
- Okunmamış mesaj sayısı
- Son mesajların üst menüde gösterilmesi
- Mesaj arama
- Mesaj silme
- Mesaj gönderim tarihi
- Gönderen ve alıcı bilgileri

### 👤 Profil

- Kullanıcı profil bilgileri
- Profil fotoğrafı yükleme
- Profil bilgilerini güncelleme
- Şifre değiştirme

### 📊 Dashboard

Dashboard üzerinde kullanıcıya ait mail ve profil bilgileri gösterilmektedir.

Dashboard içerisinde:

- Mail istatistikleri
- Kullanıcı profil özeti
- Son mesajlar
- Okunmamış mesaj bilgileri
- Haftalık mesaj aktiviteleri
- Google Gemini ile AI destekli mesaj analizi

bulunmaktadır.

### 🤖 Google Gemini AI

Dashboard üzerinde bulunan AI analiz aracı ile kullanıcının haftalık mesaj aktiviteleri analiz edilerek özetlenmektedir.

Bu özellik için:

**Google Gemini / GenAI API**

kullanılmıştır.

---

# 🛠️ Kullanılan Teknolojiler

| Teknoloji | Kullanım Alanı |
|---|---|
| **C#** | Backend geliştirme |
| **ASP.NET Core MVC (.NET 10)** | Web uygulaması ve MVC yapısı |
| **ASP.NET Core Identity** | Kullanıcı kayıt, giriş, çıkış ve şifre yönetimi |
| **Entity Framework Core** | Veritabanı işlemleri |
| **SQL Server** | Veritabanı |
| **MailKit** | SMTP üzerinden e-posta gönderimi |
| **MimeKit** | E-posta oluşturma ve yönetimi |
| **Google Gemini / GenAI API** | AI destekli haftalık mesaj analizi |
| **Mendy Admin Template** | Admin panel ve UI altyapısı |
| **Bootstrap** | Responsive arayüz |
| **JavaScript / jQuery** | Etkileşimli frontend işlemleri |
| **Summernote** | Mesaj editörü |
| **Feather Icons** | Arayüz ikonları |

---

# 🏗️ Mimari

Proje **ASP.NET Core MVC** mimarisi kullanılarak geliştirilmiştir.

### Controllers

Uygulamadaki HTTP isteklerini ve sayfa işlemlerini yönetir.

Örnek:

- `LoginController`
- `RegisterController`
- `ActivationController`
- `MessageController`
- `ProfileController`
- `DashboardController`

### Entities

Veritabanındaki temel varlıkları temsil eder.

- `AppUser`
- `Message`
- `Category`

### DTO / ViewModel

Controller ile View arasında veri taşımak için DTO ve ViewModel yapıları kullanılmıştır.

Örnek:

- Register DTO
- Login DTO
- ChangePassword DTO
- InboxViewModel
- SendMessageViewModel
- HeaderUserViewModel

### Services

Uygulamadaki servis işlemleri burada yönetilir.
GeminiAnalysisService

🖼️ Ekran Görüntüleri
<br><br>
<img src="Images/1 (1).png" width="80%" />
<br><br>
<img src="Images/1 (2).png" width="80%" />
<br><br>
<img src="Images/1 (3).png" width="80%" />
<br><br>
<img src="Images/1 (4).png" width="80%" />
<br><br>
<img src="Images/1 (5).png" width="80%" />
<br><br>
<img src="Images/1 (6).png" width="80%" />
<br><br>
<img src="Images/1 (7).png" width="80%" />
<br><br>
<img src="Images/1 (8).png" width="80%" />
<br><br>
<img src="Images/1 (9).png" width="80%" />
<br><br>
<img src="Images/1 (10).png" width="80%" />
<br><br>
<img src="Images/1 (11).png" width="80%" />
<br><br>
<img src="Images/1 (12).png" width="80%" />
<br><br>
<img src="Images/1 (13).png" width="80%" />
<br><br>
<img src="Images/1 (14).png" width="80%" />
<br><br>
<img src="Images/1 (15).png" width="80%" />
<br><br>
<img src="Images/1 (16).png" width="80%" />
<br><br>
<img src="Images/1 (17).png" width="80%" />
<br><br>
<img src="Images/1 (18).png" width="80%" />
<br><br>

