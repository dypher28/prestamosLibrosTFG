using Microsoft.UI.Xaml;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using Windows.Graphics;
using WinRT.Interop;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;



// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace prestamosLibrosTFG.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
/// 

public partial class App : MauiWinUIApplication
{
    public App()
    {
        this.InitializeComponent();
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        var mauiWindow = Microsoft.Maui.Controls.Application.Current.Windows.FirstOrDefault();
        if (mauiWindow == null)
            return;

        var nativeWindow = mauiWindow.Handler.PlatformView as Microsoft.UI.Xaml.Window;
        if (nativeWindow == null)
            return;

        IntPtr hwnd = WindowNative.GetWindowHandle(nativeWindow);
        WindowId windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
        AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

        appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
    }
}

