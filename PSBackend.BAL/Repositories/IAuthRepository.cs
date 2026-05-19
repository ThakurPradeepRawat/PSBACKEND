using PSBackend.Models;

namespace PSBackend.BAL.Repositories;

public interface IAuthRepository
{
    RegisterUserOutputModel RegisterUser(RegisterUserInputModel model);
    LoginUserOutputModel LoginUser(LoginUserInputModel model);
    GetUserByIdOutputModel GetUserById(int userId);
}
