using MediatR;
using PropertyListing.Application.Common.Models;
using PropertyListing.Application.Properties.Features.Dtos;

namespace PropertyListing.Application.Properties.Features.Queries.GetFeatures;

public record GetPropertyFeaturesQuery(Guid PropertyId) : IRequest<Result<List<PropertyFeatureDto>>>; 