namespace DXMauiApp.ViewModels;

public class MainViewModel : BaseViewModel
{
    public Command LogoutCommand { get; }

    public MainViewModel()
    {
        LogoutCommand = new Command(Logout);
    }

    private async void Logout()
    {
        await Shell.Current.GoToAsync("///LoginPage");
    }
}
