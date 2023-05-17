using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.Dtos
{
    public class ReadMovieDto
    {
        public string Title { get; set; }

        public string Genre { get; set; }

        public int Duration { get; set; }

        public DateTime QueryDateTime { get; set; } = DateTime.Now;

        public ICollection<ReadSessionDto> Sessions { get; set; }
    }
}
