using Business.Abstract;
using Entities.DTOs.BooksDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] BookCreateDto bookCreateUpdateDto)
        {
            if (bookCreateUpdateDto == null || bookCreateUpdateDto.CoverImageUrl == null || bookCreateUpdateDto.CoverImageUrl.Length == 0)
            {
                return BadRequest("Geçersiz veri");
            }
            var result=_bookService.Add(bookCreateUpdateDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _bookService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _bookService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("update")]
        public IActionResult Update(BookUpdateDto bookCreateUpdateDto)
        {
            var result = _bookService.Update(bookCreateUpdateDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var result = _bookService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("BooksShelfDetail")]
        public IActionResult GetBookShelfDetail()
        {
            var result=_bookService.GetBookShelfDetail();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("filter")]
        public IActionResult GetFilteredBooks([FromQuery] BookFilterDto filter)
        {
            var result = _bookService.GetFilter(filter);

            if (result.Success)
            {
                return Ok(result.Data); // Başarılı durumda kitapları döndür
            }

            return BadRequest(result.Message); // Başarısız durumda hata mesajını döndür
        }
    }
}
