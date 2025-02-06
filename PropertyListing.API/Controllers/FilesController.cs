using Microsoft.AspNetCore.Mvc;
using PropertyListing.Application.Common.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace PropertyListing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ApiControllerBase
{
    private readonly IFileService _fileService;
    private readonly ILogger<FilesController> _logger;

    public FilesController(IFileService fileService, ILogger<FilesController> logger)
    {
        _fileService = fileService;
        _logger = logger;
    }

    [HttpPost("upload")]
    [SwaggerOperation(
        Summary = "Dosya yükle",
        Description = "Birden fazla dosya yüklemek için kullanılır")]
    public async Task<IActionResult> Upload(IFormFileCollection files)
    {
        try
        {
            if (files == null || files.Count == 0)
                return BadRequest("Dosya seçilmedi");

            var urls = new List<string>();

            foreach (var file in files)
            {
                if (file.Length == 0)
                    continue;

                if (!file.ContentType.StartsWith("image/"))
                    return BadRequest($"'{file.FileName}' geçerli bir resim dosyası değil");

                var url = await _fileService.UploadAsync(file);
                urls.Add(url);
            }

            return Ok(new { urls });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Dosya yükleme hatası");
            return StatusCode(500, "Dosya yüklenirken bir hata oluştu");
        }
    }
} 