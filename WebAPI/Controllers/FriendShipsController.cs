using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendShipsController : ControllerBase
    {
        IFriendShipService _friendShipService;

        public FriendShipsController(IFriendShipService friendShipService)
        {
            _friendShipService = friendShipService;
        }
        [HttpPost("add")]
        public IActionResult AddFriend(int adminId,int friendId)
        {
            var userId = adminId; // Authenticated user ID
            try
            {
                var result = _friendShipService.AddFriend(userId, friendId);
                return Ok(result.Data); // FriendShip objesi döndürebilirsiniz
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("friends")]
        public IActionResult GetFriends(int adminId)
        {
            var userId = adminId; // Authenticated user ID
            var result = _friendShipService.GetFriend(userId);

            if (!result.Success)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Data);
        }

        private int GetUserId()
        {
            // Burada kimlik doğrulama mantığınıza göre kullanıcı kimliğini alma işlemini gerçekleştirin
            // Örneğin JWT token kullanıyorsanız, burada token'dan kullanıcı kimliğini çekebilirsiniz
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
