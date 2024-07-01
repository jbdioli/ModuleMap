using Microsoft.Maui.Controls.Maps;

namespace ModuleMap.Helpers.Components.Maps;

public class PinPropertyModel
{
    public PinType Type { get; set; }
    public Location Location { get; set; } = new Location();
    public string Label { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}