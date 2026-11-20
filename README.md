
# Portfolyo Projesi

ASP.NET Core MVC, Entity Framework Core (Code First) ve SQL Server ile geliştirilmiş, Admin panelinden yönetilen dinamik portfolyo sitesi.

M&Y Yazılım Eğitim Akademi - 12. Dönem Gündüz Grubu - Proje 01 (Portfolyo Case)

## Kullanılan Teknolojiler

- ASP.NET Core MVC (Areas, View Component)
- Entity Framework Core (Code First, Migrations)
- SQL Server
- Bootstrap 5, Bootstrap Icons
- SweetAlert2 (silme onayı)

## Kurulum

1. Repository'yi klonlayın:
   ```
   git clone <https://github.com/ecekarakuz/MYAcademy_PortfolioProject>
   ```
2. `Context/PortfolioContext.cs` dosyasındaki bağlantı cümlesini kendi SQL Server adınıza göre değiştirin:
   ```csharp
   optionsBuilder.UseSqlServer("Server=.\MSSQLSERVER01; Database=PortfolioDayDb; integrated security=true; Trust Server Certificate=True;");
   ```
3. Visual Studio'da Package Manager Console'u açıp veritabanını oluşturun:
   ```
   Update-Database
   ```
4. Projeyi çalıştırın (F5).
   - Portfolyo sitesi: `/`
   - Admin paneli: `/Admin/Dashboard/Index`
5. Admin panelinden içerikleri (kategori, proje, yetenek, deneyim, eğitim, sosyal medya, hakkımda, iletişim) girin. Girilen veriler siteye otomatik yansır.

Not: Proje görselleri `wwwroot/uploads` klasörüne kaydedilir, klasör ilk yüklemede otomatik oluşur.

## Yapılan Geliştirmeler

### Admin Paneli
- **Dashboard:** 7 istatistik kartı (toplam/aktif proje, yetenek, deneyim, eğitim, toplam/okunmamış mesaj), son 5 mesaj, kategori özeti (GroupBy), son 3 proje, hızlı erişim butonları. Veriler `DashboardViewModel` ile tek ekrana taşınır.
- **Proje:** CRUD, kategori dropdown'u (`SelectList` + `asp-items`), `Include` ile listede kategori adı, Aktif/Pasif alanı, tek tıkla durum değiştirme.
- **Görsel yükleme (`IFormFile`):** uzantı beyaz listesi (jpg, jpeg, png, webp), 2 MB sınırı, `Guid` ile güvenli dosya adı, veritabanında yalnızca dosya yolu. Güncellemede eski görsel diskten silinir.
- **Kategori, Yetenek (0-100 seviye), Eğitim, Deneyim, Sosyal Medya:** CRUD ekranları.
- **Hakkımda ve İletişim:** tek kayıtlı alanlar olduğu için yalnızca güncelleme ekranı.
- **Gelen Kutusu:** en yeni mesaj üstte, gönderen veya konuya göre arama, Bootstrap Modal ile sayfa yenilemeden detay, Okundu/Okunmadı durumu, okunmamış mesajların görsel olarak ayrılması, sidebar'da okunmamış mesaj sayısı, silme onayı. Mesaj içeriği düzenlenemez, yalnızca durumu yönetilir.
- **Veri doğrulama:** `[Required]`, `[StringLength]`, `[Range]`, `[EmailAddress]`, `[Url]`, `[Phone]` ve sunucu tarafında `ModelState.IsValid` kontrolü, alan bazlı hata mesajları.
- **Kullanıcı deneyimi:** tüm silme işlemleri SweetAlert2 onayı ile ve `POST` ile yapılır, işlem sonuçları `TempData` ile başarı/hata bildirimi olarak gösterilir.

### Portfolyo Sitesi (Kullanıcı Arayüzü)
- Hero, Hakkımda, Yetenekler (ilerleme çubuğu), Deneyim, Projeler, Eğitim, İletişim, Footer ve sosyal medya ikonları veritabanından beslenir (View Component).
- Pasif projeler, deneyimler ve eğitimler sitede listelenmez.
- Projelerde kategori filtresi ve en yeni projeye göre sıralama.
- İletişim formu sunucu tarafı doğrulamasıyla `Messages` tablosuna kaydedilir.
- Tekrar eden kodu azaltmak için sosyal medya ikonları tek bir View Component'ten, Create/Update formları ortak partial view'lardan gelir.

## Özgün Katkılar

- **Görsel önizleme:** Proje ve Hakkımda formlarında seçilen görsel, kaydedilmeden önce formda önizlenir.
- **Tümünü okundu işaretle:** Gelen kutusunda tek tıkla tüm mesajları okundu yapar.
- **Dashboard'dan mesaj popup'ı:** Dashboard'daki son 5 mesajdan birine tıklanınca Gelen Kutusu açılır ve mesaj popup'ı kendiliğinden açılır.
- **Sayfa yenilemeden iletişim formu:** Site formu `fetch` ile gönderilir, sunucu hataları alanların altında gösterilir, başarılı gönderimde form temizlenir.
- **Güvenlik:** Form gönderiminde `[ValidateAntiForgeryToken]` ve `[Bind]` kullanılarak dışarıdan `IsRead` gibi alanların değiştirilmesi engellenir. Mesaj içeriği popup'ta `textContent` ile yazılır, HTML olarak çalıştırılmaz.

## Bilinen Sınırlamalar

- Admin paneline giriş (kimlik doğrulama) bulunmamaktadır, case kapsamı dışında bırakılmıştır.
- Doğrulama sunucu tarafındadır, istemci tarafı (jQuery Validation) eklenmemiştir.

## Video Link

-https://youtu.be/6jYddD7g_rA

<img width="1902" height="882" alt="gelen mesajlar" src="https://github.com/user-attachments/assets/9172aa9a-10ae-4490-8ba2-c223f61e679a" />

<img width="1880" height="907" alt="dashboard" src="https://github.com/user-attachments/assets/7c37fef2-a23f-4759-b31b-1c3dcf9abcf5" />

<img width="1907" height="897" alt="anasayfa" src="https://github.com/user-attachments/assets/c3604541-d89f-4fdb-89d9-2c57d2e7c8ee" />

<img width="1832" height="777" alt="iletişim" src="https://github.com/user-attachments/assets/10335c30-f5d0-42aa-8f28-95b3a8b86baf" />



