using System.Collections.Generic;
using PSBackend.BAL.Interfaces;
using PSBackend.DAL;
using PSBackend.Models;

namespace PSBackend.BAL.Repositories
{
    public class TempleRepository : ITempleRepository
    {
        private readonly IPSDAL _sqlDataClient;

        public TempleRepository(IPSDAL psdal)
        {
            _sqlDataClient = psdal;
        }

        public List<GetAllTemplesOutputModel> GetAllTemples()
        {
            return _sqlDataClient.GetAllTemples();
        }

        public GetTempleByIdOutputModel? GetTempleById(int id)
        {
            return _sqlDataClient.GetTempleById(new GetTempleByIdInputModel { TempleId = id });
        }

        public CreateTempleOutputModel AddTemple(CreateTempleInputModel temple)
        {
            return _sqlDataClient.CreateTemple(temple);
        }
    }
}
