using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {



        //private static List<Movie> movies = new List<Movie>();
        //private static int id = 0;

        private MovieContext _context;
        private IMapper _mapper;

        public MoviesController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //todo
        //That's is not working 100%!!!!

        /// <summary>
        /// Add a movie in database
        /// </summary>
        /// <param name="movieDto">Needed Object with respective fields to be created inside database</param>
        /// <returns></returns>
        /// <response code="201">Case an addition would be finihed successfuly</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddMovie([FromBody] CreateMovieDto movieDto)
        {
            //movie.Id = id++;
            //movies.Add(movie);

            Movie movie = _mapper.Map<Movie>(movieDto);

            _context.Movies.Add(movie);
            _context.SaveChanges();

            Console.WriteLine("Movie Title ==> " + movie.Title);
            Console.WriteLine("Movie Duration ==> " + movie.Duration);

            return CreatedAtAction(nameof(GetMoviesById), new { id = movie.Id }, movie);

        }

        [HttpGet]
        public IEnumerable<ReadMovieDto> GetMovies([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            //return movies.Skip(skip).Take(take);
            //return _context.Movies.Skip(skip).Take(take);
            return _mapper.Map<List<ReadMovieDto>>(_context.Movies.Skip(skip).Take(50));
        }

        [HttpGet("{id}")]
        public IActionResult GetMoviesById(int id)
        {
            //var movie =  movies.FirstOrDefault(movie => movie.Id == id);
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (movie == null) return NotFound();

            var movieDto = _mapper.Map<ReadMovieDto>(movie);

            return Ok(movieDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDto movieDto)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id.Equals(id));

            if (movie == null) return NotFound();

            _mapper.Map(movieDto, movie);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateMoviePatch(int id, JsonPatchDocument<UpdateMovieDto> patch)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id.Equals(id));

            if (movie == null) return NotFound();

            var movieToBeUpdated = _mapper.Map<UpdateMovieDto>(movie);

            patch.ApplyTo(movieToBeUpdated, ModelState);

            if (!TryValidateModel(movieToBeUpdated))
            {
                return ValidationProblem(ModelState);
            }


            _mapper.Map(movieToBeUpdated, movie);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id.Equals(id));

            if (movie == null) return NotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
