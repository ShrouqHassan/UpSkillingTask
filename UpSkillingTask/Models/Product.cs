using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.Json.Serialization;

namespace UpSkillingTask.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Stock { get; set; }
        [JsonIgnore]
        public int? OrderId { get; set; }
        [ForeignKey("OrderId")]
        [JsonIgnore]
        public Order? Order { get; set; }
    }
}
