namespace MYAcademy_PortfolioProject.Helpers
{
    public static class ImageHelper
    {
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        private const long MaxSizeBytes = 2 * 1024 * 1024; // 2 MB

        public static string? Validate(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!AllowedExtensions.Contains(extension))
                return "Sadece .jpg, .jpeg, .png ve .webp dosyaları yüklenebilir.";

            if (!file.ContentType.StartsWith("image/"))
                return "Yüklenen dosya bir görsel olmalıdır.";

            if (file.Length > MaxSizeBytes)
                return "Dosya boyutu en fazla 2 MB olabilir.";

            return null;
        }

        public static async Task<string> SaveAsync(IFormFile file, string webRootPath, string folderName = "projects")
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var safeFileName = Guid.NewGuid().ToString("N") + extension;

            var folder = Path.Combine(webRootPath, "uploads", folderName);
            Directory.CreateDirectory(folder);

            var fullPath = Path.Combine(folder, safeFileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/uploads/" + folderName + "/" + safeFileName;
        }

        // Sadece /uploads/ altındaki (bizim yüklediğimiz) dosyaları siler
        public static void Delete(string? imagePath, string webRootPath)
        {
            if (string.IsNullOrEmpty(imagePath)) return;
            if (!imagePath.StartsWith("/uploads/")) return;

            var relativePath = imagePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
            var fullPath = Path.Combine(webRootPath, relativePath);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}