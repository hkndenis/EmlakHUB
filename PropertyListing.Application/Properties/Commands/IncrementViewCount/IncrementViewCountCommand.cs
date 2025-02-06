using MediatR;
using PropertyListing.Application.Common.Models;  // Result için
using System;

namespace PropertyListing.Application.Properties.Commands.IncrementViewCount
{
    public record IncrementViewCountCommand(Guid PropertyId) : IRequest<Result<Unit>>;
} 