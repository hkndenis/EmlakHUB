using MediatR;
using PropertyListing.Application.Common.Models;
using PropertyListing.Application.Users.Dtos;

namespace PropertyListing.Application.Common.Interfaces;

/// <summary>
/// Kimlik doğrulama ve kullanıcı yönetimi işlemlerini yöneten servis arayüzü
/// Service interface for managing authentication and user management operations
/// </summary>
public interface IIdentityService
{
    /// <summary>
    /// Yeni kullanıcı kaydeder ve JWT token döndürür
    /// Registers new user and returns JWT token
    /// </summary>
    /// <param name="email">E-posta adresi / Email address</param>
    /// <param name="password">Şifre / Password</param>
    /// <param name="firstName">Ad / First name</param>
    /// <param name="lastName">Soyad / Last name</param>
    /// <returns>JWT token içeren sonuç / Result containing JWT token</returns>
    Task<Result<string>> RegisterAsync(string email, string password, string firstName, string lastName);
    
    /// <summary>
    /// Kullanıcı girişi yapar ve JWT token döndürür
    /// Performs user login and returns JWT token
    /// </summary>
    /// <param name="email">E-posta adresi / Email address</param>
    /// <param name="password">Şifre / Password</param>
    /// <returns>JWT token içeren sonuç / Result containing JWT token</returns>
    Task<Result<string>> LoginAsync(string email, string password);
    
    /// <summary>
    /// Kullanıcının şifresini değiştirir
    /// Changes user's password
    /// </summary>
    /// <param name="userId">Kullanıcı ID'si / User ID</param>
    /// <param name="currentPassword">Mevcut şifre / Current password</param>
    /// <param name="newPassword">Yeni şifre / New password</param>
    /// <returns>İşlem sonucu / Operation result</returns>
    Task<Result<Unit>> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    
    /// <summary>
    /// E-posta adresinin benzersiz olup olmadığını kontrol eder
    /// Checks if email address is unique
    /// </summary>
    /// <param name="email">Kontrol edilecek e-posta adresi / Email address to check</param>
    /// <returns>E-posta benzersiz ise true / True if email is unique</returns>
    Task<bool> IsEmailUniqueAsync(string email);
    
    /// <summary>
    /// Kullanıcı profilini getirir
    /// Gets user profile
    /// </summary>
    /// <param name="userId">Kullanıcı ID'si / User ID</param>
    /// <returns>Kullanıcı profil bilgileri / User profile information</returns>
    Task<Result<UserProfileDto>> GetProfileAsync(Guid userId);
    
    /// <summary>
    /// Kullanıcı profilini günceller
    /// Updates user profile
    /// </summary>
    /// <param name="userId">Kullanıcı ID'si / User ID</param>
    /// <param name="firstName">Ad / First name</param>
    /// <param name="lastName">Soyad / Last name</param>
    /// <param name="phoneNumber">Telefon numarası / Phone number</param>
    /// <returns>İşlem sonucu / Operation result</returns>
    Task<Result<Unit>> UpdateProfileAsync(Guid userId, string firstName, string lastName, string phoneNumber);
} 