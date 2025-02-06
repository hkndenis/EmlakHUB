using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Properties.Queries.GetStatusHistory;

public record GetPropertyStatusHistoryQuery(Guid PropertyId) : IRequest<Result<List<StatusHistoryDto>>>; 