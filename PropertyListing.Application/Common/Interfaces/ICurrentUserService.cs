namespace PropertyListing.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string UserId { get; }
    string? Email { get; }
    string? FirstName { get; }
    string? LastName { get; }
} 