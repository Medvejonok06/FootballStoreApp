namespace FootballStoreApp.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class CreateCategoryDto
    {
        public string Name { get; set; } = null!;
    }
}
