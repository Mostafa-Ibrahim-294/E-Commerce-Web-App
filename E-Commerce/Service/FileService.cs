using E_Commerce.Service.IService;

namespace E_Commerce.Service
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public bool DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath.TrimStart('/').Replace("/", "\\"));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }

        public async Task<string> SaveFileAsync(IFormFile file , string folderPath)
        {
            if (file == null || file.Length == 0)
                return null!;
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath , folderPath);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            using (var fileStream = new FileStream(Path.Combine(uploadsFolder, fileName), FileMode.Create))
            {
              await file.CopyToAsync(fileStream);
            }
            return $"/{Path.Combine(folderPath, fileName).Replace("\\", "/")}";
        }
    }
}
