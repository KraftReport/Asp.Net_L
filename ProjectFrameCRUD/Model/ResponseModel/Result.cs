namespace ProjectFrameCRUD.Model.ResponseModel
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public bool IsFailure { get { return !IsSuccess; } }
        public string? Message { get; set; }
        public List<string>? MessageList { get; set; }
        public T? Data { get; set; }
        public string? Code { get; set; }

        public static Result<T> Success(string message,T? data = default,string? code = null)
        {
            return new Result<T>() { IsSuccess = true,Message = message, Data = data, Code = code };
        }

        public static Result<T> Fail(string message,T? data = default,string? code = null)
        {
            return new Result<T>() { IsSuccess = false, Message = message, Data = data, Code = code };
        }

    }
}
