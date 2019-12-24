namespace TryAspNetCore.Core
{
    public class ResponseResult
    {
        public bool Success { get; set; }

        public int Status { get; set; }

        public object Result { get; set; }

        public ErrorInformation Error { get; set; }
    }
}