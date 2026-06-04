using PSBackend.Models;
using System.Collections.Generic;

namespace PSBackend.BAL.Interfaces
{
    public interface IPujaRepository
    {
        List<PujaCategoryModel> GetAllPujaCategories();
        List<PujaModel> GetAllPujas();
    }
}
