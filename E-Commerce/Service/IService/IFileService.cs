namespace E_Commerce.Service.IService
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderPath);
        bool DeleteFile(string filePath);
    }
}
