using Microsoft.AspNetCore.Mvc;
using PSBackend.BAL.Repositories;

namespace PSBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FestivalsController : ControllerBase
    {
        private readonly IFestivalRepository _repository;

        public FestivalsController(IFestivalRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult GetAllFestivals([FromBody] object request)
        {
            var festivals = _repository.GetAllFestivals();
            return Ok(festivals);
        }

        [HttpPost("products")]
        public IActionResult GetAllFestivalProducts([FromBody] object request)
        {
            var products = _repository.GetAllFestivalProducts();
            return Ok(products);
        }
    }
}
