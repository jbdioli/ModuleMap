using Microsoft.Maui.Controls.Maps;

namespace ModuleMap.Helpers.Components.Maps;

public partial class PinPropertyModel : BaseModel
{
    public PinType Type { get; set; } = PinType.Place;
    public Location Location { get; set; } = new Location();
    
    [ObservableProperty] private string _label = string.Empty;
    [ObservableProperty] private string _address = string.Empty;
}