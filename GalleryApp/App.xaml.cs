namespace GalleryApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Console.WriteLine($"Unhandled exception: {e.ExceptionObject}");
                MainPage.DisplayAlert("Critical Error", "Something went wrong. Please restart the app.", "OK");
            };

            UserAppTheme = AppTheme.Dark;
        }
    }
}
