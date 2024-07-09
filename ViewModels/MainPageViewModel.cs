using Microsoft.Maui.Controls.Maps;

namespace ModuleMap.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{

    [ObservableProperty] private LocationModel _locationModel = new();
    [ObservableProperty] private Location _position = new Location();
    [ObservableProperty] private List<PinPropertyModel> _pins;
    [ObservableProperty] private PinPropertyModel _pin;
    [ObservableProperty] private bool _isMoveToPosition = false;
    [ObservableProperty] private bool _isMoveToPin = false;

    public MainPageViewModel()
    {
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

        Pin = new PinPropertyModel()
        {
            Type = PinType.Place,
            Location = new Location(37.79752, -122.40183),
            Label = "Custom Pin 1",
            Address = "Address 1"
        };
    }

    [RelayCommand]
    private Task Appearing()
    {
        IsBusy = true;

        IsMoveToPosition = true;
        IsMoveToPin =! IsMoveToPosition;

        IsBusy = false;
        return Task.CompletedTask;
    }


    [RelayCommand]
    private Task Disappearing()
    {
        return Task.CompletedTask;
    }

    [RelayCommand]
    private Task MoveToPin()
    {
        //IsMoveToPosition = false;
        //IsMoveToPin = !IsMoveToPosition;

        IsMoveToPin = true;
        IsMoveToPosition = false;

        return Task.CompletedTask;
    }
}