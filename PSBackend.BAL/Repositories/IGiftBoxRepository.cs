using PSBackend.Models;
using System.Collections.Generic;

namespace PSBackend.BAL.Repositories
{
    public interface IGiftBoxRepository
    {
        List<GiftBoxModel> GetAllGiftBoxes();
        List<GiftOccasionModel> GetAllGiftOccasions();
    }
}
