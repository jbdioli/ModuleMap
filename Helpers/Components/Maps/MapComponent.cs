using Map = Microsoft.Maui.Controls.Maps.Map;

namespace ModuleMap.Helpers.Components.Maps;

public class MapComponent : Map
{
    public List<PinPropertyModel> AddPins
    {
        get => (List<PinPropertyModel>)GetValue(PinsProperty);
        set => SetValue(PinsProperty, value);
    }


    public static readonly BindableProperty PinsProperty = BindableProperty.Create(nameof(AddPins), typeof(List<PinPropertyModel>), typeof(MapComponent), defaultValue: null);


    //public IEnumerable<PinPropertyModel> AddPins
    //{
    //    get => (IEnumerable<PinPropertyModel>)GetValue(PinsProperty);
    //    set => SetValue(PinsProperty, value);
    //}


    //public static readonly BindableProperty PinsProperty = BindableProperty.Create(nameof(AddPins), typeof(IEnumerable<PinPropertyModel>), typeof(MapComponent), defaultValue: null);

}