using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System;

namespace NoteApp
{
    class Program
    {
        public static string? FileToOpen;

        [STAThread]
        public static void Main(string[] args)
        {

            FileToOpen = args.Length > 0 ? args[0] : null;
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        private static void AppMain(Application app, string[] args)
        {
            if (app.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = new MainWindow();
                if (!string.IsNullOrEmpty(FileToOpen))
                {
                    mainWindow.OpenExistingFile(FileToOpen);
                }

                desktop.MainWindow = mainWindow;
            }
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                         .UsePlatformDetect()
                         .WithInterFont()
                         .LogToTrace();
    }
}