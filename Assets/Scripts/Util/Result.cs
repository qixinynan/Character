#nullable enable
namespace Util
{
    public enum ResultType
    {
        Ok,
        Error
    }
    public class Result<T>
    {
        public ResultType Type { get; }
        public string Message { get; }
        public T? Data { get; }

        protected Result(ResultType type, string message = "", T? data = default)
        {
            Type = type;
            Message = message;
            Data = data;
        }

        public bool IsOk => Type == ResultType.Ok;

        public static Result<T> OkResult(T data)
            => new Result<T>(ResultType.Ok, data: data);

        public static Result<T> OkMessage(string message = "")
            => new Result<T>(ResultType.Ok, message);

        public static Result<T> Error(string message)
            => new Result<T>(ResultType.Error, message);
    }

    public class Result : Result<object?>
    {
        private Result(ResultType type, string message = "")
            : base(type, message)
        {
        }

        public static new Result OkMessage(string message = "")
            => new Result(ResultType.Ok, message);

        public static new Result Error(string message)
            => new Result(ResultType.Error, message);
    }
}