
namespace ModuleMap.ViewModels;

public partial class BaseViewModel : ObservableObject
{

    [ObservableProperty]
    private bool _isBusy = false;

    [RelayCommand]
    private async Task GotoMenuPage()
    {
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync($"..");
    }

}