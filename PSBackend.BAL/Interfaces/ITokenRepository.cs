using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PSBackend.BAL.Interfaces
{
  public  interface ITokenRepository
  {
    string GenerateToken(IEnumerable<Claim> claims);
  }
}
