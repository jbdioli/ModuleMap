using Map = Microsoft.Maui.Controls.Maps.Map;

namespace ModuleMap.Helpers.Components.Maps;

public class MapComponent : Map
{
    public IEnumerable<PinPropertyModel> AddPins
    {
        get => (IEnumerable<PinPropertyModel>)GetValue(AddPinsProperty);
        set => SetValue(AddPinsProperty, value);
    }

    public PinPropertyModel AddPin
    {
        get => (PinPropertyModel)GetValue(AddPinProperty);
        set => SetValue(AddPinProperty, value);
    }

    public bool MoveToPin
    {
        get => (bool)GetValue(MoveToPinProperty);
        set => SetValue(MoveToPinProperty, value);
    }

    public bool MoveToPosition
    {
        get => (bool)GetValue(MoveToPositionProperty);
        set => SetValue(MoveToPositionProperty, value);
    }




    public static readonly BindableProperty AddPinsProperty = BindableProperty.Create(nameof(AddPins), typeof(IEnumerable<PinPropertyModel>), typeof(MapComponent), defaultValue: null);
    public static readonly BindableProperty AddPinProperty = BindableProperty.Create(nameof(AddPin), typeof(PinPropertyModel), typeof(MapComponent), defaultValue: null);
    public static readonly BindableProperty MoveToPinProperty = BindableProperty.Create(nameof(MoveToPin), typeof(bool), typeof(MapComponent), defaultValue: false);
    public static readonly BindableProperty MoveToPositionProperty = BindableProperty.Create(nameof(MoveToPosition), typeof(bool), typeof(MapComponent), defaultValue: false);


}