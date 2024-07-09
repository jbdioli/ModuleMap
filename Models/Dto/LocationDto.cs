using Map = Microsoft.Maui.Controls.Maps.Map;

namespace ModuleMap.Models.Dto;

public class LocationDto
{
    public string LocationName { get; set; } = string.Empty;
    public double Lat { get; set; }
    public double Lon { get; set; }
    public double Distance { get; set; } = 200;
    
    public string FullAddress => $"{Address}, {ZipCode} {City}, {Country}";
    public string Address { get; set; } = string.Empty;
    public string BuildingName { get; set; } = string.Empty;
    public string BuildingNumber { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string County { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    
}

