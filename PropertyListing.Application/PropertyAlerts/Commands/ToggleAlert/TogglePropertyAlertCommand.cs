using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.PropertyAlerts.Commands.ToggleAlert;

public record TogglePropertyAlertCommand(Guid Id) : IRequest<Result<Unit>>; 