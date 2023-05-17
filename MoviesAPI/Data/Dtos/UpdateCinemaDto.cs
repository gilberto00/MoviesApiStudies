using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos
{
    public class UpdateCinemaDto
    {
        [Required(ErrorMessage = "The field 'name' is mandatory")]
        public string Name { get; set; }
    }
}
