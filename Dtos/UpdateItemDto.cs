namespace FootballStoreApp.Dtos
{
    public class UpdateItemDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public int Quantity { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? CurrentOrFinalPrice { get; set; }
        public bool IsOnSale { get; set; }
        public DateTime? PurchasedDate { get; set; }
        public DateTime? SoldDate { get; set; }
        public int? CategoryId { get; set; }
    }
}
