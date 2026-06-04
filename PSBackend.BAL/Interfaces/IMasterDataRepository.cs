using PSBackend.Models;
using System.Collections.Generic;

namespace PSBackend.BAL.Interfaces
{
    public interface IMasterDataRepository
    {
        List<RatingModel> GetAllRatings();
        List<RegionModel> GetAllRegions();
    }
}
