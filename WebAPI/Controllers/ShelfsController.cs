using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs.ShelfDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelfsController : ControllerBase
    {

        IShelfService _shelfService;

        public ShelfsController(IShelfService shelfService)
        {
            _shelfService = shelfService;
        }

        [HttpPost("add")]
        public IActionResult Add(CreateShelfDto createShelfDto)
        {
            var result = _shelfService.Add(createShelfDto);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _shelfService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _shelfService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
