using System.Configuration;
using System.Data;
using System.Windows;

namespace Dashboard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Initialize the navigation service
            var navigationService = new Serviecs.NavigationService();
            // Create the main view model and pass the navigation service
            var mainViewModel = new ViewModel.MainViewModel(navigationService);
            // Create the main window and set its DataContext to the main view model
            var mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };
            // Show the main window
            mainWindow.Show();
        }
    }

}
