using System.Diagnostics;
using Location = Microsoft.Maui.Devices.Sensors.Location;

namespace ModuleMap.Shared;

public class GeolocationHandler
{

    private Location _location = new();

    private readonly LocationDto _locationDto;


    public GeolocationHandler(LocationDto locationDto)
    {
        _locationDto = locationDto;
    }


    //private GeolocationHandler()
    //{
    //}

    //public GeolocationHandler(LocationDto locationDto) : this()
    //{
    //    _locationDto = locationDto;
    //}

    //private GeolocationHandler(IGeolocation geolocation, IGeocoding geocoding, IMap map, LocationDto locationDto) : this(locationDto)
    //{
    //    _geolocation = geolocation;
    //    _geocoding = geocoding;
    //}



    public async Task GetLocationDetailsAsync()
    {
        await GetPositionAsync();
        await GetAddressDetailsByPositionAsync();

    }

    public async Task DisplayAndGetLocationDetailsAsync()
    {
        await GetPositionAsync();
        DisplayPosition();
        await GetAddressDetailsByPositionAsync();

    }


    public async Task GetPositionByAddressAsync()
    {
        //if (string.IsNullOrWhiteSpace(_locationDto.FullAddress))
        //    await Shell.Current.DisplayAlert("Error", "Please fill in street, zipcode, city and country", "OK");

        //var locations = await _geocoding.GetLocationsAsync(_locationDto.FullAddress);
        //var position = locations?.FirstOrDefault();

        //if (position != null)
        //{
        //    _locationDto.Lat = position.Latitude;
        //    _locationDto.Lon = position.Longitude;
        //}

        //await Shell.Current.DisplayAlert("Position", $"Lat: {_locationDto.Lat}, Lon: {_locationDto.Lat} ", "OK");

    }

    public void DisplayPosition()
    {
        var position = new Location(_locationDto.Lat, _locationDto.Lon);
        // move to position
        //locationDto.XNameMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(locationDto.Distance)));

    }



    public void DisplayPositionWithPin()
    {
        //if (locationDto.XNameMap == null)
        //    throw new ArgumentNullException( nameof(locationDto.XNameMap), "[ERROR] - In function DisplayPosition - Object GeolocationHandler");


        //var position = new Location(locationDto.Lat, locationDto.Lon);
        //var distance = new Distance(locationDto.Distance);
        //var locationName = string.IsNullOrWhiteSpace(locationDto.LocationName) ? _translate.GetString("LabelGeolocPosition") : locationDto.LocationName;

        //var pin = new Pin
        //{
        //    Location = position,
        //    Label = locationName
        //};


        //var mapSpan = MapSpan.FromCenterAndRadius(position, distance);

        //locationDto.XNameMap.MoveToRegion(mapSpan);
        //locationDto.XNameMap.Pins.Add(pin);
    }


    private async Task<List<string>> GetAddressesByPositionAsync()
    {
        //var position = new Location(_locationDto.Lat, _locationDto.Lon);

        //var placemarks = await _geocoding.GetPlacemarksAsync(position);
        //var placemark = placemarks?.FirstOrDefault();

        //if (placemark == null) return [];

        //var address = new List<string>
        //{
        //    placemark.AdminArea,
        //    placemark.CountryCode,
        //    placemark.CountryName,
        //    placemark.FeatureName,
        //    placemark.Locality,
        //    placemark.PostalCode,
        //    placemark.SubAdminArea,
        //    placemark.SubLocality,
        //    placemark.SubThoroughfare,
        //    placemark.Thoroughfare
        //};

        //return address;

        return new List<string>();
    }


    private async Task GetAddressDetailsByPositionAsync()
    {
        try
        {
            var placeMarks = await Geocoding.GetPlacemarksAsync(_locationDto.Lat, _locationDto.Lon);

            var placeMark = placeMarks?.FirstOrDefault();
            if (placeMark != null)
            {
                _locationDto.BuildingNumber = placeMark.SubThoroughfare;
                _locationDto.BuildingName = placeMark.FeatureName;
                _locationDto.Address = placeMark.SubThoroughfare + ' ' + placeMark.Thoroughfare;
                _locationDto.Country = placeMark.CountryName;
                _locationDto.State = placeMark.AdminArea;
                _locationDto.County = placeMark.SubAdminArea;
                _locationDto.City = placeMark.Locality;
                _locationDto.ZipCode = placeMark.PostalCode;
            }
        }
        catch (FeatureNotSupportedException fnsEx)
        {
            // Feature not supported on device
            Console.WriteLine("[ Error fnsEx ] :", fnsEx);
        }
        catch (Exception ex)
        {
            Console.WriteLine("[ Error fnsEx ] :", ex);

            // Handle exception that may have occurred in geocoding
        }
    }


    async Task GetPositionAsync()
    {
        try
        {

            var geolocation = await Geolocation.GetLocationAsync(new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.Medium,
                Timeout = TimeSpan.FromSeconds(30)
            });

            if (geolocation == null)
                await Shell.Current.DisplayAlert("Waring", "No GPS signal detected", "OK");
            else
            {
                _locationDto.Lat = geolocation.Latitude;
                _locationDto.Lon = geolocation.Longitude;
            }

        }
        catch (Exception exception)
        {
            Debug.WriteLine($"Error message from geolocation: {exception.Message}");
        }
    }
}