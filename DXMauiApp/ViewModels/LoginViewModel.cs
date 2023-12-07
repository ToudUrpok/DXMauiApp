namespace DXMauiApp.ViewModels; 

public class LoginViewModel : BaseViewModel
{
    public Command LoginCommand { get; }
    public Command SignUpCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = new Command(OnLoginClicked);
        SignUpCommand = new Command(OnSignUpClicked);
        PropertyChanged += (_, __) => LoginCommand.ChangeCanExecute();
    }

    string _userName;
    public string UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value);
    }

    string _password = string.Empty;
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    string _errorText;
    public string ErrorText
    {
        get => _errorText;
        set => SetProperty(ref _errorText, value);
    }

    bool _hasError;
    public bool HasError
    {
        get => _hasError;
        set => SetProperty(ref _hasError, value);
    }

    bool _isAuthInProcess;
    public bool IsAuthInProcess
    {
        get => _isAuthInProcess;
        set
        {
            SetProperty(ref _isAuthInProcess, value);
            OnPropertyChanged(nameof(AllowNewAuthRequests));
        }
    }

    public bool AllowNewAuthRequests
    {
        get => !IsAuthInProcess;
    }

    async void OnLoginClicked()
    {
        IsAuthInProcess = true;
        string response = await DataStore.Authenticate(UserName, Password);
        IsAuthInProcess = false;
        if (!string.IsNullOrEmpty(response))
        {
            ErrorText = response;
            HasError = true;
            return;
        }
        HasError = false;
        await Shell.Current.GoToAsync("///MainPage");
    }

    async void OnSignUpClicked()
    {
        await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Help Message", "New users are added in the Updater.UpdateDatabaseAfterUpdateSchema method", "OK");
    }
}