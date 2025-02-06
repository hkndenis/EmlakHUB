using MediatR;
using PropertyListing.Application.Common.Models;
using PropertyListing.Application.Users.Dtos;

namespace PropertyListing.Application.Users.Queries.GetProfile;

public record GetProfileQuery(Guid UserId) : IRequest<Result<UserProfileDto>>; 