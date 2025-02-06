namespace PropertyListing.Application.Users.Dtos;

public class UserProfileDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime? LastLoginDate { get; set; }
    public DateTime CreatedAt { get; set; }
} 