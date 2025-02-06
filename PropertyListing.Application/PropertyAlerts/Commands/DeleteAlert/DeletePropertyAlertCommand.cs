using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.PropertyAlerts.Commands.DeleteAlert;

public record DeletePropertyAlertCommand(Guid Id) : IRequest<Result<Unit>>; 