namespace PSBackend.Models;

public class Prasad
{
    public int PrasadId { get; set; }
    public int TempleId { get; set; }
    public int CategoryId { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? ShortDesc { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal? OriginalPrice { get; set; }
    public int? WeightGrams { get; set; }
    public int? ShelfLifeDays { get; set; }
    public string? Ingredients { get; set; }
    public bool IsGITagged { get; set; }
    public bool IsBestseller { get; set; }
    public bool IsActive { get; set; }
    public decimal AverageRating { get; set; }
    public int TotalReviews { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class GetPrasadByTempleIdInputModel
{
    public int TempleId { get; set; }
}

public class GetPrasadByTempleIdOutputModel : Prasad
{
}

public class GetPrasadByIdInputModel
{
    public int PrasadId { get; set; }
}

public class GetPrasadByIdOutputModel : Prasad
{
}

public class CreatePrasadInputModel
{
    public int TempleId { get; set; }
    public int CategoryId { get; set; }
    public string Slug { get; set; }
    public string Name { get; set; }
    public string ShortDesc { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal? OriginalPrice { get; set; }
    public int? WeightGrams { get; set; }
    public int? ShelfLifeDays { get; set; }
    public string Ingredients { get; set; }
    public bool IsGITagged { get; set; }
    public bool IsBestseller { get; set; }
}

public class CreatePrasadOutputModel
{
    public int PrasadId { get; set; }
}
