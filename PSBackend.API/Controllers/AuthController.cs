using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSBackend.BAL.Repositories;
using PSBackend.BAL.UnitOfWork;
using PSBackend.Models;
using System.Security.Claims;

namespace PSBackend.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
  private readonly IAuthRepository _authRepo;
  private readonly ITokenRepository _tokenRepo;

    public AuthController(IAuthRepository authRepository , ITokenRepository tokenRepository)
    {
        _authRepo = authRepository;
        _tokenRepo= tokenRepository;

      

    }

    [HttpPost("register")]
   [AllowAnonymous]
  public ActionResult<RegisterUserOutputModel> RegisterUser([FromBody] RegisterUserInputModel input)
    {
        try
        {
            if (input == null)
            {
                return BadRequest("User object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }


           var userId = _authRepo.RegisterUser(input);
      return userId;
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("login")]
   [AllowAnonymous]
   public ActionResult<LoginUserOutputModel> LoginUser([FromBody] LoginUserInputModel input)
    {
        try
        {
            if (input == null)
            {
                return BadRequest("Login object is null");
            }

            var result = _authRepo.LoginUser(input);
            
            if (result == null)
            {
                return Unauthorized("Invalid email or password");
            }
              var claims = new List<Claim>
                {
                };
      result.Token = _tokenRepo.GenerateToken(claims);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("GetUserById")]
    public ActionResult<GetUserByIdOutputModel> GetUserById([FromBody] GetUserByIdInputModel input)
    {
        try
        {
            if (input == null) return BadRequest("Input is null");
            var user = _authRepo.GetUserById(input.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
