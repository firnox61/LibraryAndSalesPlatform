using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs.OperationDetailDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : ControllerBase
    {
        IOperationClaimService _claim;

        public OperationClaimsController(IOperationClaimService claim)
        {
            _claim = claim;
        }

        [HttpPost("add")]
        public IActionResult Add(OperationDto operationDto)
        {
            var result = _claim.Add(operationDto);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }
        [HttpGet("claimgetall")]
        public IActionResult GetAll()
        {
            var result = _claim.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);
        }
    }
}
