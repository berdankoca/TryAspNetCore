namespace TryAspNetCore.Api.Core
{
    public class PagedResult
    {
        public int TotalCount { get; set; }

        public object Items { get; set; }
    }
}