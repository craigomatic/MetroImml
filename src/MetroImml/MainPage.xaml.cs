using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommonDX;
using Imml.IO;
using Imml.Scene.Container;
using MetroImml.Framework;
using MetroImml.Framework.Components;
using MetroImml.Input;
using MetroImml.ViewModel;
using SharpDX;
using SharpDX.Direct3D11;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MetroImml
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Windows.UI.Xaml.Media.ImageBrush _D3dBrush;
        private DeviceManager _DeviceManager;
        private SurfaceImageSourceTarget _SurfaceImageSourceTarget;

        private ImmlContextRenderer<ImmlDocument> _ContextRenderer;

        public MainPage()
        {
            this.InitializeComponent();

            (App.ViewModel.NewCommand as CommandBase).ExecuteDelegate = e => _NewFile();
            (App.ViewModel.OpenCommand as CommandBase).ExecuteDelegate = e => _OpenFile();
            (App.ViewModel.SaveCommand as CommandBase).ExecuteDelegate = e => _SaveFile();

            _ContextRenderer = new ImmlContextRenderer<ImmlDocument>(new DocumentRenderStrategy<ImmlDocument>());
            this.DataContext = App.ViewModel;
        }        

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _D3dBrush = new Windows.UI.Xaml.Media.ImageBrush();
            this.RenderTarget.Fill = _D3dBrush;

            _DeviceManager = new DeviceManager();

            var pixelWidth = (int)(this.RenderTarget.Width * DisplayProperties.LogicalDpi / 96.0);
            var pixelHeight = (int)(this.RenderTarget.Height * DisplayProperties.LogicalDpi / 96.0);

            _SurfaceImageSourceTarget = new SurfaceImageSourceTarget(pixelWidth, pixelHeight);
            _SurfaceImageSourceTarget.OnRender += _SurfaceImageSourceTarget_OnRender;
            _D3dBrush.ImageSource = _SurfaceImageSourceTarget.ImageSource;

            _DeviceManager.OnInitialize += _DeviceManager_OnInitialize;

            _DeviceManager.Initialize(DisplayProperties.LogicalDpi);

            Windows.UI.Xaml.Media.CompositionTarget.Rendering += CompositionTarget_Rendering;

            DisplayProperties.LogicalDpiChanged += DisplayProperties_LogicalDpiChanged;
        }

        void _SurfaceImageSourceTarget_OnRender(TargetBase render)
        {
            _ContextRenderer.Render(render);
        }

        void CompositionTarget_Rendering(object sender, object e)
        {
            try
            {
                _SurfaceImageSourceTarget.RenderAll();
            }
            catch { }
        }

        void DisplayProperties_LogicalDpiChanged(object sender)
        {
            _DeviceManager.Dpi = DisplayProperties.LogicalDpi;
        }

        void _DeviceManager_OnInitialize(DeviceManager obj)
        {
            _SurfaceImageSourceTarget.Initialize(obj);            
        }

        private async void _NewFile()
        {
            var immlDoc = new ImmlDocument();
            App.ViewModel.SelectedDocument = new ImmlDocumentViewModel(immlDoc);

            _ContextRenderer.Initialise(immlDoc);
        }

        private async void _SaveFile()
        {
            throw new NotImplementedException();
        }

        private async void _OpenFile()
        {
            var openPicker = new FileOpenPicker();
            openPicker.FileTypeFilter.Add(".imml");
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;

            var file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                var serialiser = new ImmlSerialiser();
                var immlDocument = await serialiser.Read<ImmlDocument>(file);

                App.ViewModel.SelectedDocument = new ImmlDocumentViewModel(immlDocument);

                _ContextRenderer.Initialise(immlDocument);
            }
        }
    }
}
