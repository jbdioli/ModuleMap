
using Microsoft.Maui.Controls.Maps;

namespace ModuleMap.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    private readonly GeolocationHandler _geolocationHandler;
    private readonly LocationDto _locationDto = new();

    [ObservableProperty] private LocationModel _locationModel = new();
    [ObservableProperty] private Location _position = new Location();
    [ObservableProperty] private List<PinPropertyModel> _pins;

    public MainPageViewModel()
    {
        _geolocationHandler = new GeolocationHandler(_locationDto);

        _pins = new List<PinPropertyModel>()
        {
             new PinPropertyModel()
             {
                 Type = PinType.Place,
                 Location = new Location(37.79752, -122.40183),
                 Label = "Custom Pin 1",
                 Address = "Address 1"
             },
             new PinPropertyModel()
             {
                 Type = PinType.Place,
                 Location = new Location(37.79852, -122.40283),
                 Label = "Custom Pin 2",
                 Address = "Address 2"
             },
        };
    }

    [RelayCommand]
    private Task Appearing()
    {
        IsBusy = true;

        _ = _geolocationHandler.DisplayAndGetLocationDetailsAsync();
        Position = new Location(_locationDto.Lat, _locationDto.Lon);

        IsBusy = false;
        return Task.CompletedTask;
    }


    [RelayCommand]
    private Task Disappearing()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private Task MoveToRegion()
    {
        return Task.CompletedTask;
    }
}