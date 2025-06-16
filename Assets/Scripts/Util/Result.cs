namespace Util
{
    public enum ResultType
    {
        Ok,
        Error
    }
    public class Result
    {
        private readonly ResultType _type;
        public readonly string Message;

        public static readonly Result Ok = new Result(ResultType.Ok);

        public Result(ResultType type, string msg = "")
        {
            _type = type;
            Message = msg;
        }

        public bool IsOk()
        {
            return _type == ResultType.Ok;
        }
    }
}