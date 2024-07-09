using System;
using System.Diagnostics;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Location = Microsoft.Maui.Devices.Sensors.Location;

namespace ModuleMap.Helpers.Components.Maps;

public class GeolocationHandler
{
    private readonly LocationPropertyModel _locationProperty;


    public GeolocationHandler(LocationPropertyModel locationProperty)
    {
        _locationProperty = locationProperty;
    }

    public async Task GetLocationDetailsAsync()
    {
        await GetPositionAsync();
        await GetAddressDetailsByPositionAsync();

    }

    public async Task DisplayAndGetLocationDetailsAsync()
    {
        await GetPositionAsync();
        MoveToCurrentPosition();
        await GetAddressDetailsByPositionAsync();

    }


    public async Task GetPositionByAddressAsync()
    {
        if (string.IsNullOrWhiteSpace(_locationProperty.FullAddress))
            await Shell.Current.DisplayAlert("Error", "Please fill in street, zipcode, city and country", "OK");

        var locations = await Geocoding.GetLocationsAsync(_locationProperty.FullAddress);
        var position = locations?.FirstOrDefault();

        if (position != null)
        {
            _locationProperty.Lat = position.Latitude;
            _locationProperty.Lon = position.Longitude;
        }

        await Shell.Current.DisplayAlert("Position", $"Lat: {_locationProperty.Lat}, Lon: {_locationProperty.Lat} ", "OK");

    }



    public async void MoveToCurrentPosition()
    {
        var geolocation = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(1));
        var location = await Geolocation.GetLocationAsync(geolocation);
        if (location != null) _locationProperty.Map.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromMeters(_locationProperty.Distance)));

    }


    public void MoveToPinPosition()
    {
        //var location = _locationProperty.PinProperty.Location;
        var dateTime = DateTime.Now;
        var dateTimeOffset = new DateTimeOffset(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, TimeSpan.FromMinutes(1));
        var location = new Location(_locationProperty.PinProperty.Location.Latitude, _locationProperty.PinProperty.Location.Longitude, dateTimeOffset);
        _locationProperty.Map.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromMeters(_locationProperty.Distance)));

    }



    public void DisplayPositionWithPin()
    {
        if (_locationProperty.Map == null)
            throw new ArgumentNullException(nameof(_locationProperty.Map), "[ERROR] - In function DisplayPosition - Object GeolocationHandler");


        var position = new Location(_locationProperty.Lat, _locationProperty.Lon);
        var distance = new Distance(_locationProperty.Distance);
        //var locationName = string.IsNullOrWhiteSpace(locationProperty.LocationName) ? _translate.GetString("LabelGeolocPosition") : locationProperty.LocationName;
        var locationName = _locationProperty.LocationName;


        var pin = new Pin
        {
            Location = position,
            Label = locationName,
            Type = _locationProperty.PinProperty.Type
        };


        var mapSpan = MapSpan.FromCenterAndRadius(position, distance);

        _locationProperty.Map.MoveToRegion(mapSpan);
        _locationProperty.Map.Pins.Add(pin);
    }


    private async Task<List<string>> GetAddressesByPositionAsync()
    {
        var position = new Location(_locationProperty.Lat, _locationProperty.Lon);

        var placemarks = await Geocoding.GetPlacemarksAsync(position);
        var placemark = placemarks?.FirstOrDefault();

        if (placemark == null) return [];

        var address = new List<string>
        {
            placemark.AdminArea,
            placemark.CountryCode,
            placemark.CountryName,
            placemark.FeatureName,
            placemark.Locality,
            placemark.PostalCode,
            placemark.SubAdminArea,
            placemark.SubLocality,
            placemark.SubThoroughfare,
            placemark.Thoroughfare
        };

        return address;

    }


    private async Task GetAddressDetailsByPositionAsync()
    {
        try
        {
            var placeMarks = await Geocoding.GetPlacemarksAsync(_locationProperty.Lat, _locationProperty.Lon);

            var placeMark = placeMarks?.FirstOrDefault();
            if (placeMark != null)
            {
                _locationProperty.BuildingNumber = placeMark.SubThoroughfare;
                _locationProperty.BuildingName = placeMark.FeatureName;
                _locationProperty.Address = placeMark.SubThoroughfare + ' ' + placeMark.Thoroughfare;
                _locationProperty.Country = placeMark.CountryName;
                _locationProperty.State = placeMark.AdminArea;
                _locationProperty.County = placeMark.SubAdminArea;
                _locationProperty.City = placeMark.Locality;
                _locationProperty.ZipCode = placeMark.PostalCode;
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
                _locationProperty.Lat = geolocation.Latitude;
                _locationProperty.Lon = geolocation.Longitude;
            }

        }
        catch (Exception exception)
        {
            Debug.WriteLine($"Error message from geolocation: {exception.Message}");
        }
    }
}