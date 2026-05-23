namespace PSBackend.Models
{
    public class RatingModel
    {
        public int Id { get; set; }
        public string FilterId { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public int Value { get; set; }
    }

    public class RegionModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
