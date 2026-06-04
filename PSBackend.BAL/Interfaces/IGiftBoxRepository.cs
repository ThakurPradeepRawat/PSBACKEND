using PSBackend.Models;
using System.Collections.Generic;

namespace PSBackend.BAL.Interfaces
{
    public interface IGiftBoxRepository
    {
        List<GiftBoxModel> GetAllGiftBoxes();
        List<GiftOccasionModel> GetAllGiftOccasions();
    }
}
