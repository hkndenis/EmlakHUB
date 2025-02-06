using MediatR;
using PropertyListing.Application.Common.Models;
using PropertyListing.Domain.Enums;  // PropertyStatus için
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyListing.Application.Properties.Commands.UpdateStatus
{
    public record UpdatePropertyStatusCommand : IRequest<Result<Unit>>
    {
        public Guid PropertyId { get; init; }
        public PropertyStatus Status { get; init; }
        public string? Note { get; init; }  // Opsiyonel not (örn: "Kapora alındı: 50.000 TL")
    }
} 