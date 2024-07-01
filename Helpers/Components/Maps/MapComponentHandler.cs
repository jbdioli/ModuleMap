using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Maps.Handlers;

namespace ModuleMap.Helpers.Components.Maps;

public partial class MapComponentHandler : MapHandler
{
    private static readonly IPropertyMapper<MapComponent, MapComponentHandler> PropertyMapper = new PropertyMapper<MapComponent, MapComponentHandler>(Mapper)
    {
        [nameof(MapComponent.AddPins)] = MapAddPins,
    };

    public MapComponentHandler() : base(PropertyMapper)
    {
    }

    private static void MapAddPins(MapComponentHandler handler, MapComponent map)
    {

        //var pins = map.Pins as List<PinPropertyModel>;

        //if (pins is not IEnumerable<PinPropertyModel> pinDetails) return;
        
        //foreach (var property in pinDetails)
        //{
        //    var pin = new Pin
        //    {
        //        Type = property.Type,
        //        Location = property.Location,
        //        Label = property.Label,
        //        Address = property.Address
        //    };

        //    map.Pins.Add(pin);
        //}
    }
}