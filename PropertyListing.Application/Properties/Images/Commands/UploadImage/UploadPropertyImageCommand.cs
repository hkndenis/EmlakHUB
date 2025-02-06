using MediatR;
using Microsoft.AspNetCore.Http;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Properties.Images.Commands.UploadImage;

public record UploadPropertyImageCommand : IRequest<Result<string>>
{
    public Guid PropertyId { get; init; }
    public IFormFile Image { get; init; }
    public bool IsMain { get; init; }
} 