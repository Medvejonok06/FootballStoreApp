namespace FootballStoreApp.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal? CurrentOrFinalPrice { get; set; }
        public bool IsOnSale { get; set; }
        public DateTime? PurchasedDate { get; set; }
        public int? CategoryId { get; set; }
    }
}
