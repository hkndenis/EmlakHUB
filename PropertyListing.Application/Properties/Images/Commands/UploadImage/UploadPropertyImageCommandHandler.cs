using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Application.Properties.Images.Commands.UploadImage;

public class UploadPropertyImageCommandHandler : IRequestHandler<UploadPropertyImageCommand, Result<string>>
{
    private readonly IFileService _fileService;
    private readonly IApplicationDbContext _context;

    public UploadPropertyImageCommandHandler(IFileService fileService, IApplicationDbContext context)
    {
        _fileService = fileService;
        _context = context;
    }

    public async Task<Result<string>> Handle(UploadPropertyImageCommand request, CancellationToken cancellationToken)
    {
        var property = await _context.Properties
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == request.PropertyId, cancellationToken);

        if (property == null)
            return Result<string>.Failure("İlan bulunamadı");

        try
        {
            var imageUrl = await _fileService.UploadAsync(request.Image);

            if (request.IsMain)
            {
                foreach (var image in property.Images.Where(i => i.IsMain))
                {
                    image.IsMain = false;
                }
            }

            var propertyImage = new PropertyImage
            {
                PropertyId = request.PropertyId,
                Url = imageUrl,
                IsMain = request.IsMain
            };

            _context.PropertyImages.Add(propertyImage);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<string>.Success(imageUrl);
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Fotoğraf yüklenirken hata oluştu: {ex.Message}");
        }
    }
} 