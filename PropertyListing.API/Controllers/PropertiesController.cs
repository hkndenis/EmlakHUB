using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;  // Authorize attribute için
using PropertyListing.Application.Properties.Commands.CreateProperty;
using PropertyListing.Application.Properties.Queries.GetProperties;
using PropertyListing.Application.Properties.Commands.UpdateProperty;
using PropertyListing.Application.Properties.Commands.DeleteProperty;
using PropertyListing.Application.Properties.Commands.UpdateStatus;
using PropertyListing.Application.Properties.Commands.IncrementViewCount;  // Yeni eklendi
using Swashbuckle.AspNetCore.Annotations;
using PropertyListing.Application.Properties.Queries.GetStatusHistory;
using PropertyListing.Application.Properties.Commands.CopyProperty;
using PropertyListing.Application.Properties.Queries.CompareProperties;
using PropertyListing.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using PropertyListing.Application.Properties.Queries.Dtos;  // PropertyDto için
using PropertyListing.Application.Properties.Queries.GetPropertyById; // GetPropertyByIdQuery için
using PropertyListing.Application.Properties.Images.Commands.UploadImage;  // UploadPropertyImageCommand için doğru namespace

namespace PropertyListing.API.Controllers;

/// <summary>
/// Emlak ilanlarını yöneten controller
/// Controller managing property listings
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ApiControllerBase
{
    private readonly IFileService _fileService;

    /// <summary>
    /// PropertiesController yapıcı metodu
    /// PropertiesController constructor
    /// </summary>
    /// <param name="fileService">Dosya işlemleri servisi / File operations service</param>
    public PropertiesController(IFileService fileService)
    {
        _fileService = fileService;
    }

    /// <summary>
    /// Emlak ilanlarını listeler. Filtreleme ve sayfalama desteği vardır.
    /// Lists property listings. Supports filtering and pagination.
    /// </summary>
    /// <param name="query">Filtreleme parametrelerini içeren sorgu / Query containing filter parameters</param>
    /// <returns>Filtrelenmiş ilan listesi / Filtered list of properties</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Emlak ilanlarını listeler", Description = "Filtreleme seçenekleriyle emlak ilanlarını getirir")]
    public async Task<IActionResult> GetProperties([FromQuery] GetPropertiesQuery query)
    {
        var result = await Mediator.Send(query);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return Ok(result.Data);
    }

    /// <summary>
    /// Yeni bir emlak ilanı oluşturur (Yetkilendirme gerektirir)
    /// Creates a new property listing (Requires authorization)
    /// </summary>
    /// <param name="command">Yeni ilan bilgilerini içeren komut / Command containing new listing information</param>
    /// <returns>Oluşturulan ilanın bilgileri / Created listing information</returns>
    [HttpPost]
    [Authorize]
    [SwaggerOperation(
        Summary = "Yeni ilan oluştur",
        Description = "Yeni bir emlak ilanı oluşturur")]
    public async Task<ActionResult<PropertyDto>> CreateProperty([FromBody] CreatePropertyCommand command)
    {
        var result = await Mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return Ok(result.Data);
    }

    /// <summary>
    /// Var olan bir emlak ilanını günceller
    /// Updates an existing property listing
    /// </summary>
    /// <param name="id">İlan ID'si / Listing ID</param>
    /// <param name="command">Güncellenmiş ilan bilgilerini içeren komut / Command containing updated listing information</param>
    /// <returns>İşlem sonucu / Operation result</returns>
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Emlak ilanını günceller", Description = "Var olan bir emlak ilanını günceller")]
    public async Task<IActionResult> Update(Guid id, UpdatePropertyCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        var result = await Mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        
        return NoContent();
    }

    /// <summary>
    /// Var olan bir emlak ilanını siler
    /// Deletes an existing property listing
    /// </summary>
    /// <param name="id">İlan ID'si / Listing ID</param>
    /// <returns>İşlem sonucu / Operation result</returns>
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Emlak ilanını siler", Description = "Var olan bir emlak ilanını siler")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeletePropertyCommand(id));
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        
        return NoContent();
    }

    /// <summary>
    /// İlanın durumunu günceller ve geçmişe kaydeder (Yetkilendirme gerektirir)
    /// Updates listing status and records to history (Requires authorization)
    /// </summary>
    /// <param name="id">İlan ID'si / Listing ID</param>
    /// <param name="command">Durum güncelleme komutunu içeren komut / Command containing status update</param>
    /// <returns>İşlem sonucu / Operation result</returns>
    [HttpPut("{id}/status")]
    [Authorize]  // Sadece giriş yapmış kullanıcılar değiştirebilsin
    [SwaggerOperation(Summary = "İlan durumunu günceller", Description = "İlanın durumunu (Available, Sold, Rented vb.) günceller ve geçmişe kaydeder")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdatePropertyStatusCommand command)
    {
        if (id != command.PropertyId)
            return BadRequest();

        var result = await Mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return NoContent();
    }

    /// <summary>
    /// İlanın durum değişikliği geçmişini listeler
    /// Lists the status change history of the listing
    /// </summary>
    /// <param name="id">İlan ID'si / Listing ID</param>
    /// <returns>Durum geçmişi listesi / Status history list</returns>
    [HttpGet("{id}/status-history")]
    [SwaggerOperation(Summary = "İlan durum geçmişini getirir", 
        Description = "İlanın durum değişikliği geçmişini listeler")]
    public async Task<IActionResult> GetStatusHistory(Guid id)
    {
        var result = await Mediator.Send(new GetPropertyStatusHistoryQuery(id));
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        
        return Ok(result.Data);
    }

    /// <summary>
    /// İlanın görüntülenme sayısını bir artırır
    /// Increments the view count of the listing
    /// </summary>
    /// <param name="id">İlan ID'si / Listing ID</param>
    /// <returns>İşlem sonucu / Operation result</returns>
    [HttpPost("{id}/view")]
    [SwaggerOperation(Summary = "İlan görüntüleme sayısını artırır", 
        Description = "İlanın görüntüleme sayısını bir artırır ve son görüntülenme tarihini günceller")]
    public async Task<IActionResult> IncrementViewCount(Guid id)
    {
        var result = await Mediator.Send(new IncrementViewCountCommand(id));
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        
        return NoContent();
    }

    /// <summary>
    /// Mevcut bir ilanı farklı bir tiple kopyalar (örn: satılık -> kiralık)
    /// Copies an existing listing with a different type (e.g., for sale -> for rent)
    /// </summary>
    /// <param name="id">Kaynak ilan ID'si / Source listing ID</param>
    /// <param name="command">Kopyalama parametrelerini içeren komut / Command containing copy parameters</param>
    /// <returns>Yeni oluşturulan ilanın bilgileri / Information of newly created listing</returns>
    [HttpPost("{id}/copy")]
    [SwaggerOperation(
        Summary = "İlanı kopyala",
        Description = "Mevcut bir ilanı farklı bir tiple (kiralık/satılık) kopyalar")]
    public async Task<IActionResult> CopyProperty(Guid id, [FromBody] CopyPropertyCommand command)
    {
        if (id != command.SourcePropertyId)
            return BadRequest();

        var result = await Mediator.Send(command);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        
        return Ok(result.Data);
    }

    /// <summary>
    /// Seçilen ilanların özelliklerini karşılaştırır
    /// Compares features of selected listings
    /// </summary>
    /// <param name="query">Karşılaştırılacak ilan ID'lerini içeren sorgu / Query containing IDs of listings to compare</param>
    /// <returns>Karşılaştırma sonuçları / Comparison results</returns>
    [HttpPost("compare")]
    [SwaggerOperation(
        Summary = "İlanları karşılaştır",
        Description = "Seçilen ilanların özelliklerini karşılaştırır")]
    public async Task<IActionResult> CompareProperties([FromBody] ComparePropertiesQuery query)
    {
        var result = await Mediator.Send(query);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        
        return Ok(result.Data);
    }

    /// <summary>
    /// İlana fotoğraf yükler
    /// Uploads photo to the listing
    /// </summary>
    /// <param name="id">İlan ID'si / Listing ID</param>
    /// <param name="file">Yüklenecek görsel dosya / Image file to upload</param>
    /// <param name="isMain">Ana görsel olup olmadığı / Whether it's the main image</param>
    /// <returns>Yüklenen görselin bilgileri / Information of uploaded image</returns>
    [HttpPost("{id}/images")]
    [SwaggerOperation(
        Summary = "Fotoğraf yükle",
        Description = "İlana fotoğraf yükler")]
    public async Task<IActionResult> UploadImage(Guid id, IFormFile file, [FromQuery] bool isMain = false)
    {
        var command = new UploadPropertyImageCommand
        {
            PropertyId = id,
            Image = file,
            IsMain = isMain
        };

        var result = await Mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Data);
    }

    /// <summary>
    /// ID'ye göre ilan detayını getirir
    /// Gets listing details by ID
    /// </summary>
    /// <param name="id">İlan ID'si / Listing ID</param>
    /// <returns>İlan detayları / Listing details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(Summary = "İlan detayı", Description = "ID'ye göre ilan detayını getirir")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await Mediator.Send(new GetPropertyByIdQuery { Id = id });
        
        if (!result.IsSuccess)
            return NotFound(new { error = result.Error });
            
        return Ok(result.Data);
    }
} 