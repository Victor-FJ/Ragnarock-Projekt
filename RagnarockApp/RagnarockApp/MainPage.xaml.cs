using System.Drawing;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using RagnarockApp.Common;
using Color = Windows.UI.Color;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RagnarockApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            MainViewModel viewModel = new MainViewModel(new NavigationService(MainPageFrame));
            this.DataContext = viewModel;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            Color backGroundColor = Color.FromArgb(255, 201, 158, 103);
            titleBar.BackgroundColor = backGroundColor;
            titleBar.ButtonBackgroundColor = backGroundColor;
        }
    }
}
