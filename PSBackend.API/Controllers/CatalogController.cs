using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSBackend.BAL.Repositories;
using PSBackend.Models;
using System.Collections.Generic;

namespace PSBackend.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly ICatalogRepository _catalogRepo;

    public CatalogController(ICatalogRepository catalogRepo)
    {
        _catalogRepo = catalogRepo;
    }

    [HttpPost("GetPrasadByTempleId")]
    [AllowAnonymous]
    public ActionResult<List<GetPrasadByTempleIdOutputModel>> GetPrasadByTempleId([FromBody] GetPrasadByTempleIdInputModel input)
    {
        try
        {
            if (input == null) return BadRequest("Input is null");
            var prasadList = _catalogRepo.GetPrasadByTempleId(input.TempleId);
            return Ok(prasadList);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("GetPrasadById")]
    [AllowAnonymous]
    public ActionResult<GetPrasadByIdOutputModel> GetPrasadById([FromBody] GetPrasadByIdInputModel input)
    {
        try
        {
            if (input == null) return BadRequest("Input is null");
            var prasad = _catalogRepo.GetPrasadById(input.PrasadId);
            if (prasad == null)
            {
                return NotFound($"Prasad with ID {input.PrasadId} not found.");
            }
            return Ok(prasad);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
     [HttpPost("GetPopularPrasad")]
     [AllowAnonymous]
     public ActionResult<GetPrasadByIdOutputModel> GetPopularPrasad()
     {
    return Ok( _catalogRepo.GetPopularPrasad());
     }


  [HttpPost]
    public ActionResult<CreatePrasadOutputModel> CreatePrasad([FromBody] CreatePrasadInputModel input)
    {
        try
        {
            if (input == null)
            {
                return BadRequest("Prasad object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var result = _catalogRepo.CreatePrasad(input);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
