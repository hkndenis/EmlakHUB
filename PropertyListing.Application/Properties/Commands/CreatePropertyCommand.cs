using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Properties.Commands;
using PropertyListing.Application.Properties.Dtos;
using PropertyListing.Domain.Entities;
using PropertyListing.Domain.Enums;
using PropertyListing.Domain.ValueObjects;
using PropertyListing.Application.Common.Models;
using AutoMapper;

namespace PropertyListing.Application.Properties.Commands
{
    public class CreatePropertyCommand : IRequest<Result<PropertyDto>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public PropertyType Type { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int SquareMeters { get; set; }
        public LocationDto Location { get; set; }
        public List<PropertyImageDto> Images { get; set; }
    }

    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, Result<PropertyDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public CreatePropertyCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Result<PropertyDto>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = new Property
            {
                Title = request.Title,
                Description = request.Description,
                Price = Money.FromDecimal(request.Price, request.Currency),
                Type = request.Type,
                Bedrooms = request.Bedrooms,
                Bathrooms = request.Bathrooms,
                SquareMeters = request.SquareMeters,
                Location = new PropertyLocation
                {
                    City = request.Location.City,
                    District = request.Location.District,
                    PostalCode = request.Location.PostalCode,
                    Latitude = request.Location.Latitude,
                    Longitude = request.Location.Longitude
                },
                Status = PropertyStatus.Available,
                UserId = _currentUserService.UserId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUserService.UserId
            };

            foreach (var image in request.Images)
            {
                property.Images.Add(new PropertyImage
                {
                    Url = image.Url,
                    IsMain = image.IsMain
                });
            }

            _context.Properties.Add(property);
            await _context.SaveChangesAsync(cancellationToken);

            return Result<PropertyDto>.Success(_mapper.Map<PropertyDto>(property));
        }
    }
} 