using PSBackend.Models;
using System.Collections.Generic;

namespace PSBackend.BAL.Repositories
{
    public interface IPujaRepository
    {
        List<PujaCategoryModel> GetAllPujaCategories();
        List<PujaModel> GetAllPujas();
    }
}
