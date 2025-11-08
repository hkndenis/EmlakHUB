namespace PropertyListing.Application.Common.Models;

/// <summary>
/// JWT token yapılandırma ayarlarını içeren sınıf
/// Class containing JWT token configuration settings
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Token imzalama için kullanılan gizli anahtar
    /// Secret key used for token signing
    /// </summary>
    public string Secret { get; set; } = string.Empty;
    
    /// <summary>
    /// Token'ı oluşturan kaynak
    /// Issuer of the token
    /// </summary>
    public string Issuer { get; set; } = string.Empty;
    
    /// <summary>
    /// Token'ın hedef kitlesi
    /// Audience of the token
    /// </summary>
    public string Audience { get; set; } = string.Empty;
    
    /// <summary>
    /// Token'ın geçerlilik süresi (dakika cinsinden)
    /// Token expiry time in minutes
    /// </summary>
    public int ExpiryMinutes { get; set; }
} 