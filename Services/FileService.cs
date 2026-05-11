using SIADAL.Interfaces;

namespace SIADAL.Services;

public class FileService : IFileService
{
    private readonly string _uploadPath;

    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx",
        ".txt", ".png", ".jpg", ".jpeg", ".zip"
    };

    public FileService(IConfiguration configuration)
    {
        _uploadPath = configuration["FileStorage:UploadPath"] ?? "/app/uploads";
        Directory.CreateDirectory(_uploadPath);
    }

    public async Task<string> SaveFileAsync(IFormFile file)
    {
        var ext = Path.GetExtension(file.FileName);
        if (!AllowedExtensions.Contains(ext))
            throw new InvalidOperationException($"Tipo de archivo no permitido: {ext}");

        var uniqueName = $"{Guid.NewGuid()}{ext}";
        var fullPath = Path.Combine(_uploadPath, uniqueName);

        await using var stream = File.Create(fullPath);
        await file.CopyToAsync(stream);

        return uniqueName;
    }

    public (Stream stream, string contentType, string fileName) GetFile(string fileName)
    {
        // Prevent path traversal
        var safeName = Path.GetFileName(fileName);
        var fullPath = Path.Combine(_uploadPath, safeName);

        if (!File.Exists(fullPath))
            throw new FileNotFoundException("Archivo no encontrado.");

        var ext = Path.GetExtension(safeName).ToLowerInvariant();
        var contentType = ext switch
        {
            ".pdf"  => "application/pdf",
            ".png"  => "image/png",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".doc"  => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xls"  => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".ppt"  => "application/vnd.ms-powerpoint",
            ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            ".txt"  => "text/plain",
            ".zip"  => "application/zip",
            _       => "application/octet-stream"
        };

        return (File.OpenRead(fullPath), contentType, safeName);
    }

    public void DeleteFile(string fileName)
    {
        var safeName = Path.GetFileName(fileName);
        var fullPath = Path.Combine(_uploadPath, safeName);
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }
}
