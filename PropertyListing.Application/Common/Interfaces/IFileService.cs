namespace PropertyListing.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

public interface IFileService
{
    Task<string> UploadAsync(IFormFile file);
    Task DeleteAsync(string url);
} 