using PSBackend.Models;
using System.Collections.Generic;

namespace PSBackend.BAL.Repositories
{
    public interface IMasterDataRepository
    {
        List<RatingModel> GetAllRatings();
        List<RegionModel> GetAllRegions();
    }
}
