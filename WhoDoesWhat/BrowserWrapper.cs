using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.Diagnostics;

namespace WhoDoesWhat
{
    public class BrowserHelper
    {
        public string RootPath { get; set; } = string.Empty;

        private WebView2 webView21;

        public BrowserHelper()
        {
        }

        public async Task Attach(WebView2 browserControl)
        {
            webView21 = browserControl;
            webView21.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;
            await webView21.EnsureCoreWebView2Async(null);
        }

        bool initialized = false;

        public void PrintWithUI()
        {
            // More options here : https://github.com/MicrosoftDocs/edge-developer/blob/main/microsoft-edge/webview2/how-to/print.md
            webView21.CoreWebView2.ShowPrintUI();
        }


        private void CoreWebView2_PermissionRequested(object? sender, CoreWebView2PermissionRequestedEventArgs e)
        {
            throw new NotImplementedException();
        }


        public bool NavComplete { get; set; }

        private void CoreWebView2_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            NavComplete = true;
        }

        string showUrlOnceInitialized = string.Empty;
        public void ShowUrl(string url)
        {
            if (!initialized)
            {
                showUrlOnceInitialized = url;
                return;
            }

            NavComplete = false;
            webView21.CoreWebView2.Navigate(url);
        }

        // ShowFile
        public void ShowFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                RootPath = Path.GetDirectoryName(fileName);
            }
            else
                return;

            NavComplete = false;

            ShowUrl($"file:///{fileName}");
        }

        public void ShowHtml(string html)
        {
            NavComplete = false;
            webView21.CoreWebView2.NavigateToString(html);
        }

        private void WebView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            Debug.WriteLine("WebView_CoreWebView2InitializationCompleted");
            initialized = true;

            webView21.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;
            webView21.CoreWebView2.PermissionRequested += CoreWebView2_PermissionRequested;

            if (!string.IsNullOrEmpty(showUrlOnceInitialized))
            {
                //webView21.Width = 2000;

                webView21.CoreWebView2.Navigate(showUrlOnceInitialized);
                showUrlOnceInitialized = string.Empty;
            }

        }

        // https://learn.microsoft.com/en-us/microsoft-edge/webview2/concepts/working-with-local-content?tabs=dotnetcsharp#loading-local-content-by-handling-the-webresourcerequested-event
    }
}
