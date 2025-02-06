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

[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ApiControllerBase
{
    private readonly IFileService _fileService;

    public PropertiesController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Emlak ilanlarını listeler", Description = "Filtreleme seçenekleriyle emlak ilanlarını getirir")]
    public async Task<IActionResult> GetProperties([FromQuery] GetPropertiesQuery query)
    {
        var result = await Mediator.Send(query);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
            
        return Ok(result.Data);
    }

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

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Emlak ilanını siler", Description = "Var olan bir emlak ilanını siler")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeletePropertyCommand(id));
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        
        return NoContent();
    }

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