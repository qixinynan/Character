namespace Util
{
    public enum ResultType
    {
        Ok,
        Error
    }
    public class Result
    { 
        public readonly ResultType Type;
        public string Message;

        public static Result Ok = new Result(ResultType.Ok);

        public Result(ResultType type, string msg = "")
        {
            Type = type;
            Message = msg;
        }

        public bool IsOk()
        {
            return Type == ResultType.Ok;
        }
    }
}