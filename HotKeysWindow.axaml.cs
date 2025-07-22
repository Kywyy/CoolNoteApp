using Avalonia.Controls;
using Avalonia.Interactivity;

namespace NoteApp;

public partial class HotKeysWindow : Window
{
    public HotKeysWindow()
    {
        InitializeComponent();
    }

    private void OkClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}