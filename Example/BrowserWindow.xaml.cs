using Microsoft.Web.WebView2.Core;
using System;
using System.Windows;

namespace Example
{
    /// <summary>
    /// Interaction logic for BrowserWindow.xaml
    /// </summary>
    public partial class BrowserWindow : Window
    {
        public event Action<string> OnNavigated;

        public void Navigate(string url)
        {
            Browser.Source = new Uri(url);
        }

        public BrowserWindow()
        {
            InitializeComponent();

            Loaded += BrowserWindow_Loaded;
        }

        private void BrowserWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Browser.NavigationCompleted += Browser_NavigationCompleted; ;
            Browser.SourceChanged += Browser_SourceChanged;
        }

        private void Browser_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            Browser.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
        }

        private void CoreWebView2_NewWindowRequested(object? sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Handled = true;

            // Josh: if you want pop-ups to navigate the main window instead of just be supressed, uncomment
            // below: 
            //Browser.Source = new Uri(e.Uri);
        }

        private void Browser_SourceChanged(object? sender, CoreWebView2SourceChangedEventArgs e)
        {
            Title = Browser.CoreWebView2.DocumentTitle;
            AddressBar.Text = Browser.Source.ToString();

            OnNavigated?.Invoke(Browser.Source.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Browser.Source = new Uri(AddressBar.Text);
        }
    }
}
