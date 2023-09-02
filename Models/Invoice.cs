using System.ComponentModel.DataAnnotations;

namespace Dashboard.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        public string CustomerId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public int QTY { get; set; }

        public double Tax { get; set; }

        public double Discount { get; set; }

        public double Total { get; set; }

    }
}
