using System.Data;
using PSBackend.Models;

namespace PSBackend.BAL.Interfaces;

public interface ITempleRepository
{
    List<GetAllTemplesOutputModel> GetAllTemples();
    GetTempleByIdOutputModel? GetTempleById(int id);
    CreateTempleOutputModel AddTemple(CreateTempleInputModel temple);
}
