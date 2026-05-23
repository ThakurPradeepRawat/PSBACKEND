using Microsoft.AspNetCore.Mvc;
using PSBackend.BAL.Repositories;

namespace PSBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IMasterDataRepository _repository;

        public MasterDataController(IMasterDataRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("ratings")]
        public IActionResult GetAllRatings([FromBody] object request)
        {
            var ratings = _repository.GetAllRatings();
            return Ok(ratings);
        }

        [HttpPost("regions")]
        public IActionResult GetAllRegions([FromBody] object request)
        {
            var regions = _repository.GetAllRegions();
            return Ok(regions);
        }
    }
}
