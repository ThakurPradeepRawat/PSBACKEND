using Microsoft.AspNetCore.Mvc;
using PSBackend.BAL.Repositories;

namespace PSBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PujasController : ControllerBase
    {
        private readonly IPujaRepository _repository;

        public PujasController(IPujaRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("categories")]
        public IActionResult GetAllPujaCategories([FromBody] object request)
        {
            var categories = _repository.GetAllPujaCategories();
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult GetAllPujas([FromBody] object request)
        {
            var pujas = _repository.GetAllPujas();
            return Ok(pujas);
        }
    }
}
