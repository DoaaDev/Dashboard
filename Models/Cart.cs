using System.ComponentModel.DataAnnotations;

namespace Dashboard.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public string CostumerId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public int QTY { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }

    }
}
