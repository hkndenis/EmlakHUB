namespace PropertyListing.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

/// <summary>
/// Dosya yükleme ve silme işlemlerini yöneten servis arayüzü
/// Service interface for managing file upload and delete operations
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Dosyayı yükler ve URL'ini döndürür
    /// Uploads file and returns its URL
    /// </summary>
    /// <param name="file">Yüklenecek dosya / File to upload</param>
    /// <returns>Yüklenen dosyanın URL'i / URL of the uploaded file</returns>
    Task<string> UploadAsync(IFormFile file);
    
    /// <summary>
    /// Belirtilen URL'deki dosyayı siler
    /// Deletes file at the specified URL
    /// </summary>
    /// <param name="url">Silinecek dosyanın URL'i / URL of the file to delete</param>
    Task DeleteAsync(string url);
} 