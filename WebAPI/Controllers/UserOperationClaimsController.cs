using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs.OperationDetailDto;
using Entities.DTOs.ShelfDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : ControllerBase
    {
        IUserOperationClaimService _claim;

        public UserOperationClaimsController(IUserOperationClaimService claim)
        {
            _claim = claim;
        }

        [HttpPost("add")]
        public IActionResult Add(UserOperationDto userOperationDto)
        {
            var result = _claim.Add(userOperationDto);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }
        [HttpGet("getallUserClaim")]
        public IActionResult getAll()
        {
            var result = _claim.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
    }
}
