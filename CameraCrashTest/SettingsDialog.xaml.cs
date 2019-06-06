using System.Linq;
using Windows.UI.Xaml.Controls;

namespace CameraCrashTest
{
    public sealed partial class SettingsDialog : ContentDialog
    {
        public MainPageViewModel ViewModel => DataContext as MainPageViewModel;

        public SettingsDialog()
        {
            InitializeComponent();
        }

        private async void SettingsDialog_OnOpened(ContentDialog sender, ContentDialogOpenedEventArgs args)
        {
            await ViewModel.Categories.First().Init();
        }
    }
}
