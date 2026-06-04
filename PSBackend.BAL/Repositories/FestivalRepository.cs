using PSBackend.BAL.Interfaces;
using PSBackend.DAL;
using PSBackend.Models;
using System.Collections.Generic;

namespace PSBackend.BAL.Repositories
{
    public class FestivalRepository : IFestivalRepository
    {
        private readonly IPSDAL _dal;

        public FestivalRepository(IPSDAL dal)
        {
            _dal = dal;
        }

        public List<FestivalModel> GetAllFestivals()
        {
            return _dal.GetAllFestivals();
        }

        public List<FestivalProductModel> GetAllFestivalProducts()
        {
            return _dal.GetAllFestivalProducts();
        }
    }
}
