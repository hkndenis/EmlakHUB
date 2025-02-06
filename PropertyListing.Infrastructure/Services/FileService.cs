using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PropertyListing.Application.Common.Interfaces;
using System.IO;
using System.Threading;

namespace PropertyListing.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _env;
    private readonly string _uploadDirectory = "uploads";

    public FileService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> UploadAsync(IFormFile file)
    {
        var uploadsPath = Path.Combine(_env.WebRootPath, _uploadDirectory);
        
        // Uploads klasörünü oluştur
        if (!Directory.Exists(uploadsPath))
            Directory.CreateDirectory(uploadsPath);

        // Benzersiz dosya adı oluştur
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadsPath, fileName);

        // Dosyayı kaydet
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Dosya URL'ini döndür
        return $"/{_uploadDirectory}/{fileName}";
    }

    public Task DeleteAsync(string url)
    {
        if (string.IsNullOrEmpty(url))
            return Task.CompletedTask;

        var fileName = Path.GetFileName(url);
        var filePath = Path.Combine(_env.WebRootPath, _uploadDirectory, fileName);

        if (File.Exists(filePath))
            File.Delete(filePath);

        return Task.CompletedTask;
    }
} 