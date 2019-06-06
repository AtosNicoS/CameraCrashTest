using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CameraCrashTest
{
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel ViewModel => DataContext as MainPageViewModel;

        private ContentDialog dialog;

        public MainPage()
        {
            this.InitializeComponent();

            DataContext = new MainPageViewModel();
        }

        private async void Button_OnClick(object sender, RoutedEventArgs e)
        {

            //await ContentDialog.ShowAsync();

            // BUG: Caused by creating the dialog in code
            var dialog = new SettingsDialog();
            //(Grid) this.Content).Children.Add(dialog);
            await dialog.ShowAsync();
           //(Grid)this.Content).Children.Remove(dialog);
        }
    }
}
