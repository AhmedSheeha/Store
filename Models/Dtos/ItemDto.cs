using System.ComponentModel.DataAnnotations;

namespace Store.Models.Dtos
{
    public class ItemDto
    {
       
            [MaxLength(100)]
            public string Name { get; set; }
            public double Price { get; set; }
            public string? Notes { get; set; }
            public Byte[] Image { get; set; }
            public int CategoryId { get; set; }
        
    }
}
