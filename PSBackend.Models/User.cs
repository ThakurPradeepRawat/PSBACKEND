using System.Numerics;

namespace PSBackend.Models;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; }
    public bool IsEmailVerified { get; set; }
    public bool IsPhoneVerified { get; set; }
    public bool IsActive { get; set; }
    public string? OtpCode { get; set; }
    public DateTime? OtpExpiry { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
}

public class  RegisterUserInputModel
 {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string  PasswordHash { get; set; }
    public string ProfileImageUrl { get; set; }
}

public class RegisterUserOutputModel
{
  public int UserId { get; set; }

}

public class GetUserByEmailInputModel
{
    public string Email { get; set; }
}

public class GetUserByEmailOutputModel 
{
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; }
}

public class GetUserByIdInputModel
{
    public int UserId { get; set; }
}

public class GetUserByIdOutputModel 
{
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? ProfileImageUrl { get; set; }
}

public class LoginUserInputModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginUserOutputModel
{
    public string Token { get; set; }
    public GetUserByIdOutputModel User { get; set; }
}
