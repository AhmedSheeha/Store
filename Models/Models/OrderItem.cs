using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Store.Models.Models;

namespace Store.Data.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Order? orders { get; set; }

        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Item? items { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}