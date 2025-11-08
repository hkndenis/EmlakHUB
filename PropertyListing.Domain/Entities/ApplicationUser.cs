using PropertyListing.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PropertyListing.Domain.Entities;

/// <summary>
/// Uygulama kullanıcısını temsil eden entity. Identity framework ile entegre.
/// Entity representing application user. Integrated with Identity framework.
/// </summary>
public class ApplicationUser : IdentityUser<string>
{
    /// <summary>
    /// Kullanıcının e-posta adresi
    /// User's email address
    /// </summary>
    public new string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// Kullanıcının şifrelenmiş parola hash'i
    /// User's encrypted password hash
    /// </summary>
    public new string PasswordHash { get; set; } = string.Empty;
    
    /// <summary>
    /// Kullanıcının adı
    /// User's first name
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
    
    /// <summary>
    /// Kullanıcının soyadı
    /// User's last name
    /// </summary>
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>
    /// Kullanıcının telefon numarası
    /// User's phone number
    /// </summary>
    public new string PhoneNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// E-posta adresinin doğrulanıp doğrulanmadığı
    /// Whether the email address has been confirmed
    /// </summary>
    public new bool EmailConfirmed { get; set; }
    
    /// <summary>
    /// Telefon numarasının doğrulanıp doğrulanmadığı
    /// Whether the phone number has been confirmed
    /// </summary>
    public new bool PhoneNumberConfirmed { get; set; }
    
    /// <summary>
    /// Kullanıcının son giriş tarihi ve saati
    /// Date and time of user's last login
    /// </summary>
    public DateTime? LastLoginDate { get; set; }
    
    /// <summary>
    /// Kullanıcı kaydını oluşturan kişinin kimliği
    /// Identifier of the person who created the user record
    /// </summary>
    public string? CreatedBy { get; set; }
    
    /// <summary>
    /// Kullanıcı kaydını son değiştiren kişinin kimliği
    /// Identifier of the person who last modified the user record
    /// </summary>
    public string? LastModifiedBy { get; set; }
    
    /// <summary>
    /// Kullanıcı kaydının oluşturulma tarihi ve saati
    /// Date and time when the user record was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Kullanıcı kaydının son değiştirilme tarihi ve saati
    /// Date and time when the user record was last modified
    /// </summary>
    public DateTime? LastModifiedAt { get; set; }
    
    // Navigation properties
    
    /// <summary>
    /// Kullanıcının oluşturduğu emlak ilanları
    /// Property listings created by the user
    /// </summary>
    public ICollection<Property> Properties { get; private set; } = new List<Property>();
    
    /// <summary>
    /// Kullanıcının favori ilanları
    /// User's favorite listings
    /// </summary>
    public ICollection<Favorite> Favorites { get; private set; } = new List<Favorite>();
} 