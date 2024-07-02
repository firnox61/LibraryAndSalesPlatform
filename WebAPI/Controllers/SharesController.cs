using Business.Abstract;
using Entities.DTOs.BooksDetail;
using Entities.DTOs.ShareDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharesController : ControllerBase
    {
        IShareService _shareService;

        public SharesController(IShareService shareService)
        {
            _shareService = shareService;
        }
        [HttpPost("add")]
        public IActionResult Add(CreateShareDto createShareDto)
        {
            var result = _shareService.Add(createShareDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _shareService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _shareService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("update")]
        public IActionResult Update(CreateShareDto createShareDto)
        {
            var result = _shareService.Update(createShareDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var result = _shareService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
