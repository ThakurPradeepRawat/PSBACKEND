using System.Collections.Generic;
using PSBackend.BAL.Interfaces;
using PSBackend.DAL;
using PSBackend.Models;

namespace PSBackend.BAL.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly IPSDAL _sqlDataClient;

        public CatalogRepository(IPSDAL psdal)
        {
            _sqlDataClient = psdal;
        }

        public CreatePrasadOutputModel CreatePrasad(CreatePrasadInputModel prasad)
        {
            return _sqlDataClient.CreatePrasad(prasad);
        }

        public GetPrasadByIdOutputModel? GetPrasadById(int prasadId)
        {
            return _sqlDataClient.GetPrasadById(new GetPrasadByIdInputModel { PrasadId = prasadId });
        }

        public List<GetPrasadByTempleIdOutputModel> GetPrasadByTempleId(int templeId)
        {
            return _sqlDataClient.GetPrasadByTempleId(new GetPrasadByTempleIdInputModel { TempleId = templeId });
        }
        public List<GetPrasadByIdOutputModel> GetPopularPrasad()
        {
            return _sqlDataClient.GetPopularPrasad();
        }
    }
}
