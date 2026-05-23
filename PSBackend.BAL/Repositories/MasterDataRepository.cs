using PSBackend.DAL;
using PSBackend.Models;
using System.Collections.Generic;

namespace PSBackend.BAL.Repositories
{
    public class MasterDataRepository : IMasterDataRepository
    {
        private readonly IPSDAL _dal;

        public MasterDataRepository(IPSDAL dal)
        {
            _dal = dal;
        }

        public List<RatingModel> GetAllRatings()
        {
            return _dal.GetAllRatings();
        }

        public List<RegionModel> GetAllRegions()
        {
            return _dal.GetAllRegions();
        }
    }
}
