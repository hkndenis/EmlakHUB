using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Properties.Images.Commands.DeleteImage;

public class DeletePropertyImageCommandHandler : IRequestHandler<DeletePropertyImageCommand, Result>
{
    private readonly IFileService _fileService;
    private readonly IApplicationDbContext _context;

    public DeletePropertyImageCommandHandler(IFileService fileService, IApplicationDbContext context)
    {
        _fileService = fileService;
        _context = context;
    }

    public async Task<Result> Handle(DeletePropertyImageCommand request, CancellationToken cancellationToken)
    {
        var propertyImage = await _context.PropertyImages.FindAsync(request.ImageId);
        if (propertyImage == null)
            return Result.Failure("Fotoğraf bulunamadı");

        try
        {
            await _fileService.DeleteAsync(propertyImage.Url);

            _context.PropertyImages.Remove(propertyImage);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Fotoğraf silinirken hata oluştu: {ex.Message}");
        }
    }
} 