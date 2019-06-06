using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Media.Capture;
using Windows.UI.Xaml.Controls;
using Panel = Windows.Devices.Enumeration.Panel;

namespace CameraCrashTest
{
    public class MainPageViewModel : BindableBase
    {
        public MainPageViewModel()
        {
            _categories = new ObservableCollection<PivotViewModel>();
            Categories = new ReadOnlyObservableCollection<PivotViewModel>(_categories);

            _categories.Add(new PivotViewModel());
        }

        public ReadOnlyObservableCollection<PivotViewModel> Categories { get; }
        private readonly ObservableCollection<PivotViewModel> _categories;

        public PivotViewModel SelectedCategory
        {
            get => _selectedCategory;
            set => Set(ref _selectedCategory, value);
        }
        private PivotViewModel _selectedCategory;
    }

    public class PivotViewModel : BindableBase
    {
        public MediaCapture MediaSource
        {
            get => _mediaSource;
            private set => Set(ref _mediaSource, value);
        }

        private MediaCapture _mediaSource;

        public async Task Init()
        {
            //throw new NotImplementedException();

            await Task.Delay(1000);

            // Get the camera devices
            var cameraDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            // try to get the back facing device for a phone
            var backFacingDevice = cameraDevices
                .FirstOrDefault(c => c.EnclosureLocation?.Panel == Panel.Back);

            // but if that doesn't exist, take the default or first camera device available
            var SelectedVideoDevice = backFacingDevice ??
                                      cameraDevices.FirstOrDefault(x => x.IsDefault) ?? cameraDevices.FirstOrDefault();



            // Create MediaCapture
            var mediaCapture = new MediaCapture();

            // Initialize MediaCapture and settings
            await mediaCapture.InitializeAsync(
                new MediaCaptureInitializationSettings
                {
                    VideoDeviceId = SelectedVideoDevice.Id
                });

            //// TODO debug
            //mediaCapture.CameraStreamStateChanged += MediaCaptureOnCameraStreamStateChanged;
            //mediaCapture.Failed += MediaCapture_Failed;
            //mediaCapture.CaptureDeviceExclusiveControlStatusChanged += MediaCaptureOnCaptureDeviceExclusiveControlStatusChanged;

            // Set the preview source for the CaptureElement
            MediaSource = mediaCapture;

            // Start viewing through the CaptureElement 
            await mediaCapture.StartPreviewAsync();
        }
    }
}
