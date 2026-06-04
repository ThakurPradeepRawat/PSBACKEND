using System.Collections.Generic;
using System.Linq;

namespace PSBackend.Models
{
    public class GiftOccasionModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? Description { get; set; }
    }

    public class GiftBoxModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string? BgGradient { get; set; }
        public string? Tag { get; set; }
        public string? TagClass { get; set; }
        public decimal Price { get; set; }
        public string? PriceNote { get; set; }
        public bool IsPopular { get; set; }
        public string? ButtonClass { get; set; }
       
        public string? Contents { get; set; }
     
        public List<string> ContentList 
        { 
            get 
            {
                if (string.IsNullOrEmpty(Contents)) return new List<string>();
                return Contents.Split('|').ToList();
            } 
        }
    }
}
