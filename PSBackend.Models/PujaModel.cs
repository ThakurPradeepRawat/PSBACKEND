namespace PSBackend.Models
{
    public class PujaCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? BgColor { get; set; }
        public int Count { get; set; }
    }

    public class PujaModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? TempleName { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string? BgGradient { get; set; }
        public string? Duration { get; set; }
        public bool IncludesCertificate { get; set; }
        public bool IncludesVideo { get; set; }
        public string? Tag { get; set; }
        public string? TagClass { get; set; }
        public decimal Price { get; set; }
        public string? PriceNote { get; set; }
    }
}
