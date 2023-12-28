namespace DotnetAngularMiniEcommerce_API.Application.Requestparameters
{
    public record Pagination
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
