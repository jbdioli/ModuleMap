namespace ModuleMap.Models.Model;

public partial class LocationModel : BaseModel
{
    [ObservableProperty] private string _locationName = string.Empty;
    [ObservableProperty] private double _lat = 0;
    [ObservableProperty] private double _lon = 0;
    [ObservableProperty] private double _distance = 200;
    
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(FullAddress))] private string _address = string.Empty;
    [ObservableProperty] private string _buildingName = string.Empty;
    [ObservableProperty] private string _buildingNumber = string.Empty;
    [ObservableProperty] private string _country = string.Empty;
    [ObservableProperty] private string _state = string.Empty;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(FullAddress))] private string _county = string.Empty;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(FullAddress))] private string _city = string.Empty;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(FullAddress))] private string _zipCode = string.Empty;
    public string FullAddress => $"{_address}, {_zipCode} {_city}, {_country}";

}

