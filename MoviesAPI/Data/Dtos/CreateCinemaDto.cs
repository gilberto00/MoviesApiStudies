using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos
{
    public class CreateCinemaDto
    {

        [Required(ErrorMessage = "The field 'name' is mandatory")]
        public string Name { get; set; }
        public int AddressId { get; set; }
    }
}
