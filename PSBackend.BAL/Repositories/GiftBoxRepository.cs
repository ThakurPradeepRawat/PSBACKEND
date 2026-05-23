using PSBackend.DAL;
using PSBackend.Models;
using System.Collections.Generic;

namespace PSBackend.BAL.Repositories
{
    public class GiftBoxRepository : IGiftBoxRepository
    {
        private readonly IPSDAL _dal;

        public GiftBoxRepository(IPSDAL dal)
        {
            _dal = dal;
        }

        public List<GiftBoxModel> GetAllGiftBoxes()
        {
            return _dal.GetAllGiftBoxes();
        }

        public List<GiftOccasionModel> GetAllGiftOccasions()
        {
            return _dal.GetAllGiftOccasions();
        }
    }
}
