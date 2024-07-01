using Business.Abstract;
using Entities.DTOs.BooksDetail;
using Entities.DTOs.NoteDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {

        INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpPost("add")]
        public IActionResult Add(CreateNoteDTo createNoteDTo)
        {
            var result = _noteService.Add(createNoteDTo);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _noteService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _noteService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("update")]
        public IActionResult Update(CreateNoteDTo createNoteDTo)
        {
            var result = _noteService.Update(createNoteDTo);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var result = _noteService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
