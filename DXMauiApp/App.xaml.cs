using DXMauiApp.Services;

namespace DXMauiApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<WebAPIService>();

            MainPage = new AppShell();
            //Shell.Current.GoToAsync("LoginPage");
        }
    }
}