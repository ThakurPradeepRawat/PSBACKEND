using System.Data;
using PSBackend.Models;

namespace PSBackend.BAL.Repositories;

public interface ITempleRepository
{
    List<GetAllTemplesOutputModel> GetAllTemples();
    GetTempleByIdOutputModel? GetTempleById(int id);
    CreateTempleOutputModel AddTemple(CreateTempleInputModel temple);
}
