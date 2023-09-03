using System.ComponentModel.DataAnnotations;

namespace Dashboard.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string HolderName { get; set; }
        [Required]
        public int CardNumber { get; set; }
        [Required]
        public string Exp { get; set; }

        public string CustomerId { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }
    }
}
