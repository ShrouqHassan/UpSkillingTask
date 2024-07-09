using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UpSkillingTask.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; } 
        public decimal TotalAmount { get; set; }
        public virtual ICollection <Product>? Products { get; set; }
    }
}
