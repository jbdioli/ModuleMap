using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps.Handlers;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace ModuleMap.Helpers.Components.Maps;

public partial class MapComponentHandler : MapHandler
{
    private static readonly IPropertyMapper<MapComponent, MapComponentHandler> PropertyMapper = new PropertyMapper<MapComponent, MapComponentHandler>(Mapper)
    {
        [nameof(MapComponent.AddPins)] = MapAddPins,
        [nameof(MapComponent.AddPin)] = MapAddPin,
        [nameof(MapComponent.MoveToPosition)] = MapMoveToPosition,
        [nameof(MapComponent.MoveToPinProperty)] = MapMoveToPin,
    };


    private static readonly LocationPropertyModel LocationProperty = new LocationPropertyModel(new Map());
    private static GeolocationHandler _geolocationHandler = new GeolocationHandler(LocationProperty);

    public MapComponentHandler() : base(PropertyMapper)
    {
    }

    private static void MapAddPins(MapComponentHandler handler, MapComponent map)
    {
        LocationProperty.Map = map;

        var pins = map.AddPins as List<PinPropertyModel>;

        if (pins is not IEnumerable<PinPropertyModel> pinDetails) return;

        foreach (var property in pinDetails)
        {
            var pin = new Pin
            {
                Type = property.Type,
                Location = property.Location,
                Label = property.Label,
                Address = property.Address
            };

            map.Pins.Add(pin);
        }
    }


    private static void MapAddPin(MapComponentHandler handler, MapComponent map)
    {
        LocationProperty.Map = map;

        if (map.AddPin == null)
        {
            return;
        }

        var pin = new Pin
        {
            Type = map.AddPin.Type,
            Location = map.AddPin.Location,
            Label = map.AddPin.Label,
            Address = map.AddPin.Address
        };

        map.Pins.Add(pin);
    }


    private static void MapMoveToPosition(MapComponentHandler handler, MapComponent map)
    {
        LocationProperty.Map = map;

        var isPosition = map.MoveToPosition is bool ? (bool)map.MoveToPosition : false;
        if (isPosition)
        {
            _geolocationHandler.MoveToCurrentPosition();
        }
    }


    private static void MapMoveToPin(MapComponentHandler handler, MapComponent map)
    {

        LocationProperty.Map = map;

        var isPosition = map.MoveToPin is bool ? (bool)map.MoveToPin : false;

        if (map.AddPin != null)
        {
            LocationProperty.PinProperty = map.AddPin;
        }
        else if (map.Pins != null && map.AddPins.Any())
        {
            LocationProperty.PinProperty = map.AddPins.First();
        }
        else
        {
            return;
        }

        if (isPosition)
        {
            
            _geolocationHandler.MoveToPinPosition();
        }
    }


}