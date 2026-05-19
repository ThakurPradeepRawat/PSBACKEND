namespace PSBackend.Models;

public class Temple
{
    public int TempleId { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Deity { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string PinCode { get; set; } = string.Empty;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string CoverImageUrl { get; set; } = string.Empty;
    public decimal AverageRating { get; set; }
    public int TotalReviews { get; set; }
    public bool IsVerified { get; set; }
    public bool IsActive { get; set; }
    public string Tags { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class GetAllTemplesOutputModel : Temple
{
}

public class GetTempleByIdInputModel
{
    public int TempleId { get; set; }
}

public class GetTempleByIdOutputModel : Temple
{
}

public class CreateTempleInputModel
{
    public string Slug { get; set; }
    public string Name { get; set; }
    public string Deity { get; set; }
    public string Description { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PinCode { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string CoverImageUrl { get; set; }
    public string Tags { get; set; }
}

public class CreateTempleOutputModel
{
    public int TempleId { get; set; }
}

public class GetAllTemplesInputModel
{
}
