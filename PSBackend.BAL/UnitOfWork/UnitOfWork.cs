using DataAccessLayer;
using PSBackend.BAL.Repositories;
using PSBackend.DAL;
using PSBackend.Models;

namespace PSBackend.BAL.UnitOfWork;

public class RegisterUserUow
{
  private readonly IPSDAL _sqlDataClient;
  private readonly RegisterUserInputModel _input;
  public RegisterUserOutputModel? output;

  public RegisterUserUow(IPSDAL sqlDataClient, RegisterUserInputModel input)
  {
    _sqlDataClient = sqlDataClient;
    _input = input;
    Sql();
  }
  public  void Sql()
  {
    output = _sqlDataClient.Registeruser(_input);
  }
}






