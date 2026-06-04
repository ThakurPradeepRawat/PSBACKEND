using PSBackend.Models;

namespace PSBackend.BAL.Interfaces;

public interface IAuthRepository
{
    RegisterUserOutputModel RegisterUser(RegisterUserInputModel model);
    LoginUserOutputModel? LoginUser(LoginUserInputModel model);
    GetUserByIdOutputModel GetUserById(int userId);
}
