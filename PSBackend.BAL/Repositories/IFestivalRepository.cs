using PSBackend.Models;
using System.Collections.Generic;

namespace PSBackend.BAL.Repositories
{
    public interface IFestivalRepository
    {
        List<FestivalModel> GetAllFestivals();
        List<FestivalProductModel> GetAllFestivalProducts();
    }
}
