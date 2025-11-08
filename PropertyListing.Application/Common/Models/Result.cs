namespace PropertyListing.Application.Common.Models;

/// <summary>
/// İşlem sonucunu temsil eden sınıf (veri içermeden)
/// Class representing operation result (without data)
/// </summary>
public class Result
{
    /// <summary>
    /// İşlemin başarılı olup olmadığını belirtir
    /// Indicates whether the operation was successful
    /// </summary>
    public bool IsSuccess { get; }
    
    /// <summary>
    /// Hata durumunda hata mesajı
    /// Error message in case of failure
    /// </summary>
    public string Error { get; }
    
    /// <summary>
    /// İşlemin başarısız olup olmadığını belirtir
    /// Indicates whether the operation failed
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Result sınıfının yapıcı metodu
    /// Constructor for Result class
    /// </summary>
    /// <param name="isSuccess">Başarı durumu / Success status</param>
    /// <param name="error">Hata mesajı / Error message</param>
    protected Result(bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Başarılı sonuç oluşturur
    /// Creates successful result
    /// </summary>
    /// <returns>Başarılı sonuç nesnesi / Successful result object</returns>
    public static Result Success() => new Result(true, string.Empty);
    
    /// <summary>
    /// Başarısız sonuç oluşturur
    /// Creates failed result
    /// </summary>
    /// <param name="error">Hata mesajı / Error message</param>
    /// <returns>Başarısız sonuç nesnesi / Failed result object</returns>
    public static Result Failure(string error) => new Result(false, error);
}

/// <summary>
/// İşlem sonucunu ve veriyi temsil eden generic sınıf
/// Generic class representing operation result with data
/// </summary>
/// <typeparam name="T">Döndürülecek veri tipi / Type of data to return</typeparam>
public class Result<T> : Result
{
    /// <summary>
    /// İşlem sonucu döndürülen veri
    /// Data returned by the operation
    /// </summary>
    public T Data { get; }

    /// <summary>
    /// Result&lt;T&gt; sınıfının yapıcı metodu
    /// Constructor for Result&lt;T&gt; class
    /// </summary>
    /// <param name="isSuccess">Başarı durumu / Success status</param>
    /// <param name="error">Hata mesajı / Error message</param>
    /// <param name="data">Döndürülecek veri / Data to return</param>
    protected Result(bool isSuccess, string error, T data) 
        : base(isSuccess, error)
    {
        Data = data;
    }

    /// <summary>
    /// Başarılı sonuç ve veri oluşturur
    /// Creates successful result with data
    /// </summary>
    /// <param name="data">Döndürülecek veri / Data to return</param>
    /// <returns>Başarılı sonuç nesnesi / Successful result object</returns>
    public static Result<T> Success(T data) => new Result<T>(true, string.Empty, data);
    
    /// <summary>
    /// Başarısız sonuç oluşturur
    /// Creates failed result
    /// </summary>
    /// <param name="error">Hata mesajı / Error message</param>
    /// <returns>Başarısız sonuç nesnesi / Failed result object</returns>
    public static Result<T> Failure(string error) => new Result<T>(false, error, default!);
} 