using System.Windows;

namespace Example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BrowserWindow _window1 = null, _window2 = null;

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 1. instantiate the two separate windows
            _window1 = new BrowserWindow();
            _window2 = new BrowserWindow();

            // 2. show window 1. 
            _window1.Show();
            _window2.Show();

            // 3a. for the purposes of this demo, I only want to know when window 1 navigates to a page
            _window1.OnNavigated += _window1_OnNavigated;
            // 3b. start navigating 
            _window1.Navigate("https://darwin.v7labs.com/workview?dataset=285490&image=68393");
        }

        private void _window1_OnNavigated(string url)
        {
            _window2.Navigate(url);
        }
    }
}
