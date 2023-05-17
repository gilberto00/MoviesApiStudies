using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Session
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
