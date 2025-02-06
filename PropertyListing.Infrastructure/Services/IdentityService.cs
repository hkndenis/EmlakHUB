using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PropertyListing.Application.Common.Interfaces;
using PropertyListing.Application.Common.Models;
using PropertyListing.Application.Users.Dtos;
using PropertyListing.Domain.Entities;

namespace PropertyListing.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly IApplicationDbContext _context;
    private readonly JwtSettings _jwtSettings;

    public IdentityService(IApplicationDbContext context, IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Result<string>> RegisterAsync(string email, string password, string firstName, string lastName)
    {
        try
        {
            if (!await IsEmailUniqueAsync(email))
            {
                return Result<string>.Failure("Bu email adresi zaten kullanımda");
            }

            var user = new ApplicationUser
            {
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                FirstName = firstName,
                LastName = lastName,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(CancellationToken.None);

            var token = GenerateJwtToken(user);
            return Result<string>.Success(token);
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Kayıt işlemi sırasında bir hata oluştu: {ex.Message}");
        }
    }

    public async Task<Result<string>> LoginAsync(string email, string password)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return Result<string>.Failure("Email veya şifre hatalı");

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return Result<string>.Failure("Email veya şifre hatalı");

            user.LastLoginDate = DateTime.UtcNow;
            await _context.SaveChangesAsync(CancellationToken.None);

            var token = GenerateJwtToken(user);
            return Result<string>.Success(token);
        }
        catch (Exception ex)
        {
            return Result<string>.Failure($"Giriş işlemi sırasında bir hata oluştu: {ex.Message}");
        }
    }

    public async Task<Result<Unit>> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
            return Result<Unit>.Failure("User not found.");

        if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
            return Result<Unit>.Failure("Current password is incorrect.");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        user.LastModifiedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(CancellationToken.None);

        return Result<Unit>.Success(Unit.Value);
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !await _context.Users.AnyAsync(u => u.Email == email);
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<Result<UserProfileDto>> GetProfileAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
            return Result<UserProfileDto>.Failure("User not found.");

        var userIdString = user.Id.ToString();
        var parsedUserId = Guid.Parse(userIdString);

        return Result<UserProfileDto>.Success(new UserProfileDto
        {
            Id = parsedUserId,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            LastLoginDate = user.LastLoginDate,
            CreatedAt = user.CreatedAt
        });
    }

    public async Task<Result<Unit>> UpdateProfileAsync(Guid userId, string firstName, string lastName, string phoneNumber)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
            return Result<Unit>.Failure("User not found.");

        user.FirstName = firstName;
        user.LastName = lastName;
        user.PhoneNumber = phoneNumber;
        user.LastModifiedAt = DateTime.UtcNow;
        user.LastModifiedBy = "system";

        await _context.SaveChangesAsync(CancellationToken.None);

        return Result<Unit>.Success(Unit.Value);
    }
} 