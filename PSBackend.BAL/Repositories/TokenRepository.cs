using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PSBackend.BAL.Repositories
{
  public  class TokenRepository:ITokenRepository
  {
    private readonly IConfiguration _configuration;

    public TokenRepository(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public string GenerateToken(IEnumerable<Claim> claims)
    {
      var key = new SymmetricSecurityKey(
          Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is missing.")));

      var creds = new SigningCredentials(
          key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
          issuer: _configuration["Jwt:Issuer"],
          audience: _configuration["Jwt:Audience"],
          claims: claims,
          expires: DateTime.UtcNow.AddMinutes(30),
          signingCredentials: creds
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
