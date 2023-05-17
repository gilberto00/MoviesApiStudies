using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MoviesAPI.Data;
using MoviesAPI.Data.Dtos;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private MovieContext _movieContext;
        private IMapper _mapper;

        public AddressController(MovieContext context, IMapper mapper)
        {
            _movieContext = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaAddAddress([FromBody] CreateAddressDto addressDto)
        {
            Address address = _mapper.Map<Address>(addressDto);
            _movieContext.Addresses.Add(address);
            _movieContext.SaveChanges();
            return CreatedAtAction(nameof(RecuperaAddresssPorId), new { Id = address.Id }, address);
        }

        [HttpGet]
        public IEnumerable<ReadAddressDto> RecuperaAddresss()
        {
            return _mapper.Map<List<ReadAddressDto>>(_movieContext.Addresses);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaAddresssPorId(int id)
        {
            Address address = _movieContext.Addresses.FirstOrDefault(address => address.Id == id);
            if (address != null)
            {
                ReadAddressDto addressDto = _mapper.Map<ReadAddressDto>(address);

                return Ok(addressDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaAddress(int id, [FromBody] UpdateAddressDto addressDto)
        {
            Address address = _movieContext.Addresses.FirstOrDefault(address => address.Id == id);
            if (address == null)
            {
                return NotFound();
            }
            _mapper.Map(addressDto, address);
            _movieContext.SaveChanges();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletaAddress(int id)
        {
            Address address = _movieContext.Addresses.FirstOrDefault(address => address.Id == id);
            if (address == null)
            {
                return NotFound();
            }
            _movieContext.Remove(address);
            _movieContext.SaveChanges();
            return NoContent();
        }
    }
}
