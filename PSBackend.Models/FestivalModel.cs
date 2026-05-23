namespace PSBackend.Models
{
    public class FestivalModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public string? Tag { get; set; }
        public string? TagClass { get; set; }
        public string? DateBgColor { get; set; }
        public string? DateTextColor { get; set; }
    }

    public class FestivalProductModel
    {
        public int Id { get; set; }
        public string FestivalName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Badge { get; set; }
        public string? BadgeClass { get; set; }
        public string? Icon { get; set; }
        public string? BgGradient { get; set; }
        public string? ButtonText { get; set; }
    }
}
