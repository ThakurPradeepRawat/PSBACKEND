using PSBackend.Models;
using System.Collections.Generic;

namespace PSBackend.BAL.Interfaces
{
    public interface IFestivalRepository
    {
        List<FestivalModel> GetAllFestivals();
        List<FestivalProductModel> GetAllFestivalProducts();
    }
}
