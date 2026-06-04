using PSBackend.BAL.Interfaces;
using PSBackend.DAL;
using PSBackend.Models;
using System.Collections.Generic;

namespace PSBackend.BAL.Repositories
{
    public class PujaRepository : IPujaRepository
    {
        private readonly IPSDAL _dal;

        public PujaRepository(IPSDAL dal)
        {
            _dal = dal;
        }

        public List<PujaCategoryModel> GetAllPujaCategories()
        {
            return _dal.GetAllPujaCategories();
        }

        public List<PujaModel> GetAllPujas()
        {
            return _dal.GetAllPujas();
        }
    }
}
