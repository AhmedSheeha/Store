namespace Store.Models.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> productNames { get; set; } = new();
    }
}
