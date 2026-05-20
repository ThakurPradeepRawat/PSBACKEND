using PSBackend.DAL;
using PSBackend.Models;
using BCrypt;

namespace PSBackend.BAL.Repositories
{
  public class AuthRepository : IAuthRepository
  {
    private readonly IPSDAL _sqlDataClient;
   

    public AuthRepository(IPSDAL psdal)
    {
      _sqlDataClient = psdal;
    }

    public RegisterUserOutputModel RegisterUser(
        RegisterUserInputModel model)
    {
      model.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.PasswordHash);
      return _sqlDataClient.Registeruser(model);
    }

    public LoginUserOutputModel? LoginUser(LoginUserInputModel model)
    {
        var user = _sqlDataClient.GetUserByEmail(new GetUserByEmailInputModel { Email = model.Email });

      if (user == null || string.IsNullOrWhiteSpace(user.PasswordHash))
        {
            return null;
        }

       bool IsValid = BCrypt.Net.BCrypt.Verify( model.Password, user.PasswordHash);

      if (!IsValid)
        {
            return null; // Or throw exception based on convention
        }

        return new LoginUserOutputModel
        {
            Token = string.Empty,
            User = user
        };
    }

    public GetUserByIdOutputModel GetUserById(int userId)
    {
        return _sqlDataClient.GetUserById(new GetUserByIdInputModel { UserId = userId });
    }
  }
}
