namespace SIADAL.Interfaces;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file);
    (Stream stream, string contentType, string fileName) GetFile(string fileName);
    void DeleteFile(string fileName);
}
