using MediatR;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Addresses.Queries.GetCoordinates;

public class GetCoordinatesQueryHandler : IRequestHandler<GetCoordinatesQuery, Result<CoordinatesDto>>
{
    private readonly IGeocodingService _geocodingService;

    public GetCoordinatesQueryHandler(IGeocodingService geocodingService)
    {
        _geocodingService = geocodingService;
    }

    public async Task<Result<CoordinatesDto>> Handle(GetCoordinatesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var (latitude, longitude, formattedAddress) = await _geocodingService.GetCoordinatesAsync(
                request.Address,
                request.City,
                request.District);

            return Result<CoordinatesDto>.Success(new CoordinatesDto
            {
                Latitude = latitude,
                Longitude = longitude,
                FormattedAddress = formattedAddress
            });
        }
        catch (Exception ex)
        {
            return Result<CoordinatesDto>.Failure($"Geocoding failed: {ex.Message}");
        }
    }
} 