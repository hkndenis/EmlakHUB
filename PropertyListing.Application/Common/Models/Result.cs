namespace PropertyListing.Application.Common.Models;

public class Result
{
    public bool IsSuccess { get; }
    public string Error { get; }
    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new Result(true, string.Empty);
    public static Result Failure(string error) => new Result(false, error);
}

public class Result<T> : Result
{
    public T Data { get; }

    protected Result(bool isSuccess, string error, T data) 
        : base(isSuccess, error)
    {
        Data = data;
    }

    public static Result<T> Success(T data) => new Result<T>(true, string.Empty, data);
    public static Result<T> Failure(string error) => new Result<T>(false, error, default!);
} 