using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AdminPanelBackend.Models;
using AdminPanelBackend.Services;


namespace AdminPanelBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
            
                return BadRequest(ModelState);
            }

            var result = await _userService.Register(registerModel);

            if (result)
            {
                return Ok(new { message = "Kullanıcı başarıyla kaydedildi." });
            }
            else
            {
                return BadRequest(new { message = "Kullanıcı adı veya e-posta zaten kullanılıyor." });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _userService.Login(model);
            if (result)
                return Ok("Giriş başarılı.");
            return Unauthorized("Geçersiz kullanıcı adı veya şifre.");
        }
    }
}
