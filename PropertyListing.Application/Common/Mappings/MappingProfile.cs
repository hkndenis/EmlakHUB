using AutoMapper;
using PropertyListing.Application.Properties.Features.Dtos;
using PropertyListing.Application.Properties.Queries.Dtos;
using PropertyListing.Application.Properties.Favorites.Dtos;
using PropertyListing.Application.Properties.Queries.GetStatusHistory;
using PropertyListing.Application.PropertyAlerts.Queries.GetUserAlerts;
using PropertyListing.Domain.Entities;
using PropertyListing.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace PropertyListing.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Property, PropertyDto>()
            .ForMember(d => d.Price, opt => opt.MapFrom(s => EF.Property<decimal>(s.Price, "Amount")))
            .ForMember(d => d.Currency, opt => opt.MapFrom(s => EF.Property<string>(s.Price, "Currency")))
            .ForMember(d => d.Location, opt => opt.MapFrom(s => s.Location));

        CreateMap<PropertyLocation, LocationDto>();
        CreateMap<PropertyFeature, PropertyFeatureDto>();
        CreateMap<PropertyImage, PropertyImageDto>();
        CreateMap<Favorite, FavoriteDto>();
        CreateMap<PropertyStatusHistory, StatusHistoryDto>();
        CreateMap<PropertyAlert, PropertyAlertDto>();
    }
} 