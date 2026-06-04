using Microsoft.AspNetCore.Mvc;
using PSBackend.BAL.Interfaces;

namespace PSBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftBoxesController : ControllerBase
    {
        private readonly IGiftBoxRepository _repository;

        public GiftBoxesController(IGiftBoxRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult GetAllGiftBoxes([FromBody] object request)
        {
            var boxes = _repository.GetAllGiftBoxes();
            return Ok(boxes);
        }

        [HttpPost("occasions")]
        public IActionResult GetAllGiftOccasions([FromBody] object request)
        {
            var occasions = _repository.GetAllGiftOccasions();
            return Ok(occasions);
        }
    }
}
