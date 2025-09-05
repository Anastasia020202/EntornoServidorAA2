using Microsoft.AspNetCore.Mvc;
using ParkingApp2.Data.Repositories;

namespace ParkingApp2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlazasController : ControllerBase
    {
        private readonly IPlazaRepository _plazaRepository;

        public PlazasController(IPlazaRepository plazaRepository)
        {
            _plazaRepository = plazaRepository;
        }
    }
}
