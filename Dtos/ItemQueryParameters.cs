namespace FootballStoreApp.Dtos
{
    public class ItemQueryParameters
    {
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? SortBy { get; set; }
        public bool Descending { get; set; } = false;
        
    }
}
