namespace ELProjectsApp.Shared.Kernel;

public class Result
{
    public Error Error { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
            throw new ArgumentException("Invalid error assigment.", nameof(error));

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);

    public static implicit operator Result(Error error) => Failure(error);
}

public class Result<TValue> : Result
{
    public TValue? Value { get; }

    protected Result(TValue value) : base(true, Error.None)
    {
        if (value is null)
            throw new ArgumentException("Invalid value assigment.", nameof(value));
        Value = value;
    }
    protected Result(Error error) : base(false, error)
    {
    }

    public static Result<TValue> Success(TValue value) => new Result<TValue>(value);
    public static new Result<TValue> Failure(Error error) => new Result<TValue>(error);
    public static Result<TValue> Create(TValue? value) =>
        value is not null ? Success(value) : Failure(Error.NullValue);
    public static implicit operator Result<TValue>(TValue value) => new Result<TValue>(value);
    public static implicit operator Result<TValue>(Error error) => new Result<TValue>(error);
}

public sealed class Result<TValue, TError> : Result<TValue>
    where TError : Error
{

    private Result(TError error) : base(error) { }
    private Result(TValue value) : base(value) { }
    public static Result<TValue, TError> Failure(TError error) => new Result<TValue, TError>(error);

    public static implicit operator Result<TValue, TError>(TError error) => new Result<TValue, TError>(error);
    public static implicit operator Result<TValue, TError>(TValue value) => new Result<TValue, TError>(value);
}