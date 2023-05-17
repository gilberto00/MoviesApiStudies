using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private MovieContext _movieContext;
        private IMapper _mapper;

        public CinemaController(MovieContext movieContext, IMapper mapper)
        {
            _movieContext = movieContext;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _movieContext.Cinemas.Add(cinema);
            _movieContext.SaveChanges();

            return CreatedAtAction(nameof(GetCinemaById), new { Id = cinema.Id }, cinemaDto);
        }

        //[HttpGet]
        //public IEnumerable<ReadCinemaDto> GetCinema()
        //{
        //    var cinemaList = _mapper.Map<List<ReadCinemaDto>>(_movieContext.Cinemas.ToList());

        //    return cinemaList;
        //}
        [HttpGet]
        public IEnumerable<ReadCinemaDto> GetCinema([FromQuery] int? addressId = null)
        {
            if (addressId == null)
            {
                return _mapper.Map<List<ReadCinemaDto>>(_movieContext.Cinemas.ToList());
            }

            return _mapper.Map<List<ReadCinemaDto>>(_movieContext.Cinemas.FromSqlRaw($"SELECT Id, Name, AddressId FROM Cinemas WHERE Cinemas.AddressId = {addressId}").ToList());

            var cinemaList = _mapper.Map<List<ReadCinemaDto>>(_movieContext.Cinemas.ToList());

            return cinemaList;
        }

        [HttpGet("{id}")]
        public IActionResult GetCinemaById(int id)
        {
            Cinema cinema = _movieContext.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            }

            return NotFound();
        }

        [HttpPut]
        public IActionResult UpdateCinema(int id, [FromBody] UpdateCinemaDto updateCinemaDto)
        {
            Cinema cinema = _movieContext.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }

            _mapper.Map(updateCinemaDto, cinema);
            _movieContext.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteCinema(int id)
        {
            Cinema cinema = _movieContext.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }

            _movieContext.Remove(cinema);
            _movieContext.SaveChanges();
            return NoContent();
        }

    }
}
