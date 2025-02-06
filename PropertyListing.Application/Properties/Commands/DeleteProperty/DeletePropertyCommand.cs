using MediatR;
using PropertyListing.Application.Common.Models;

namespace PropertyListing.Application.Properties.Commands.DeleteProperty;

public record DeletePropertyCommand(Guid Id) : IRequest<Result<Unit>>; 