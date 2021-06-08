namespace DietPlanner.DTO.Response
{
    public record Response<T>(T Data, int StatusCode, bool IsSuccessful, Error ErrorData)
    {
        public static Response<T> Success(T data, int statusCode=200) => new(data, statusCode, true, null);

        public static Response<T> Success(int statusCode=200) => new(default, statusCode, true, null);

        public static Response<T> Fail(int statusCode, bool isShow, string path, params string[] errors)
            => new(default, statusCode, false, Error.SendError(path, isShow, errors));

        public static Response<T> Fail<D>(Response<D> response)
            => new(default, response.StatusCode, response.IsSuccessful, response.ErrorData);
    }
}
