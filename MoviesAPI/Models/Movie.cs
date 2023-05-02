using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "The movie title is mandatory")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The movie genre title is mandatory")]
        [MaxLength(50, ErrorMessage ="The lenght cannot be greater than 50 characters")]
        public string Genre { get; set; }

        [Required]
        [Range(70, 600, ErrorMessage ="The duration must be between 70 and 600 minutes")]
        public int Duration { get; set; }
    }
}
