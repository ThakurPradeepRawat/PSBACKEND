using PSBackend.Models;

namespace PSBackend.BAL.Interfaces;

public interface ICatalogRepository
{
    CreatePrasadOutputModel CreatePrasad(CreatePrasadInputModel prasad);
    GetPrasadByIdOutputModel? GetPrasadById(int prasadId);
    List<GetPrasadByTempleIdOutputModel> GetPrasadByTempleId(int templeId);
  List<GetPrasadByIdOutputModel> GetPopularPrasad();
}
