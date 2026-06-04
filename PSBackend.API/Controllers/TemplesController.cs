using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSBackend.BAL.Interfaces;
using PSBackend.Models;
using System.Collections.Generic;

namespace PSBackend.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TemplesController : ControllerBase
{
    private readonly ITempleRepository _templeRepo;

    public TemplesController(ITempleRepository templeRepo)
    {
        _templeRepo = templeRepo;
    }

    [HttpPost("GetAllTemples")]
    [AllowAnonymous]
    public ActionResult<List<GetAllTemplesOutputModel>> GetAllTemples([FromBody] GetAllTemplesInputModel input)
    {
        try
        {
            var temples = _templeRepo.GetAllTemples();
            return Ok(temples);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("GetTempleById")]
    [AllowAnonymous]
    public ActionResult<GetTempleByIdOutputModel> GetTempleById([FromBody] GetTempleByIdInputModel input)
    {
        try
        {
            if (input == null) return BadRequest("Input is null");
            var temple = _templeRepo.GetTempleById(input.TempleId);
            if (temple == null)
            {
                return NotFound($"Temple with ID {input.TempleId} not found.");
            }
            return Ok(temple);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public ActionResult<CreateTempleOutputModel> CreateTemple([FromBody] CreateTempleInputModel input)
    {
        try
        {
            if (input == null)
            {
                return BadRequest("Temple object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var result = _templeRepo.AddTemple(input);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
