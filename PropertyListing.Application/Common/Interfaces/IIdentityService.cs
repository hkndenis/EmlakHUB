using MediatR;
using PropertyListing.Application.Common.Models;
using PropertyListing.Application.Users.Dtos;

namespace PropertyListing.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<Result<string>> RegisterAsync(string email, string password, string firstName, string lastName);
    Task<Result<string>> LoginAsync(string email, string password);
    Task<Result<Unit>> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    Task<bool> IsEmailUniqueAsync(string email);
    Task<Result<UserProfileDto>> GetProfileAsync(Guid userId);
    Task<Result<Unit>> UpdateProfileAsync(Guid userId, string firstName, string lastName, string phoneNumber);
} 