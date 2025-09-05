using Microsoft.AspNetCore.Mvc;
using ParkingApp2.Business.Services;

namespace ParkingApp2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
    }
}
