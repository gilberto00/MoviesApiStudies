using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Address_1 { get; set; }
        public int Number { get; set; }
        public virtual Cinema Cinema { get; set; }
    }
}
