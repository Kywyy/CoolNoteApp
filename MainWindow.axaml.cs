using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
using Avalonia.Styling;
using System;
using System.IO;


namespace NoteApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        TextWrapCheckBox.IsCheckedChanged += TextWrapCheckBox_IsCheckedChanged;
        ThemeChangeButton.IsCheckedChanged += ThemeChangeButton_IsChekedChanged;

        NightTheme();
    }

    //nova topBar
    private void Minimize_Click(object? sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
    private void Maximize_Click(object? sender, RoutedEventArgs e)
    {
        if (WindowState == WindowState.Maximized)
        {
            WindowState = WindowState.Normal;
        }
        else
        {
            WindowState = WindowState.Maximized;
        }
    }
    private void Close_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    public void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var window = TopLevel.GetTopLevel(this) as MainWindow;
        if (window == null) return;

        Point mouse_pos = e.GetPosition(this);
        PixelPoint mouse_pos_pixelPoint = new PixelPoint((int)mouse_pos.X, (int)mouse_pos.Y);
        var movingMouseAsset = AssetLoader.Open(new System.Uri("avares://NoteApp/Assets/MouseMoveArrow.png"));
        var bitmap = new Bitmap(movingMouseAsset);

        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            Cursor = new Cursor(bitmap, mouse_pos_pixelPoint);
            window.BeginMoveDrag(e);
        }
    }
    public void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        Cursor = new Cursor(StandardCursorType.Arrow);
    }



    //first divider
    private void NewMenu_Click(object? sender, RoutedEventArgs e)
    {
        var new_Note = new MainWindow();
        new_Note.Show();
    }

    private IStorageFile? current_File = null;
    private string? file_Name;
    private async void OpenMenu_Click(object? sender, RoutedEventArgs e)
    {
        var files = await this.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "Select...",
            FileTypeFilter = new[] { FilePickerFileTypes.TextPlain, FilePickerFileTypes.ImageAll }
        });

        if (files.Count > 0)
        {
            var file = files[0];
            current_File = file;
            file_Name = current_File.Name;
            await using var streamer = await file.OpenReadAsync();
            using var reader = new StreamReader(streamer);
            string file_Text = await reader.ReadToEndAsync();
            MainText.Text = file_Text;
        }
    }
    public void OpenExistingFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            string content = File.ReadAllText(filePath);
            MainText.Text = content;
        }
    }

    private void SaveMenu_Click(object? sender, RoutedEventArgs e)
    {
        string saveFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NoteApp");
        Directory.CreateDirectory(saveFolder);
        if (file_Name == null)
        {
            saveFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NoteApp", "NewNote.txt");
            string filePath = Path.Combine(saveFolder);
            File.WriteAllText(filePath, MainText.Text);
        }
        else
        {
            saveFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NoteApp", file_Name);
            string filePath = Path.Combine(saveFolder);
            File.WriteAllText(filePath, MainText.Text);
        }
    }

    private async void SaveAsMenu_Click(object? sender, RoutedEventArgs e)
    {
        var file = await this.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions()
        {
            Title = "Save as...",
            SuggestedFileName = "NewNote.txt",
            FileTypeChoices = new[]
            {
                new FilePickerFileType("Text Files"){
                    Patterns = new[] {"*.txt"}
                },
                new FilePickerFileType("Image Files"){
                    Patterns = new[] {"*.png", "*.jpg", "*.jpeg", "*.gif", "*.bmp",  "*.webp"}
                },
            }
        });
        if (file != null)
        {
            await using var streamer = await file.OpenWriteAsync();
            using var file_writer = new StreamWriter(streamer);
            await file_writer.WriteAsync(MainText.Text);
        }
    }

    private void TextWrapCheckBox_IsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (TextWrapCheckBox.IsChecked == true) MainText.TextWrapping = Avalonia.Media.TextWrapping.Wrap;
        else { MainText.TextWrapping = Avalonia.Media.TextWrapping.NoWrap; }
    }
    private void ThemeChangeButton_IsChekedChanged(object? sender, RoutedEventArgs e)
    {

        if (ThemeChangeButton.IsChecked == true)
        {
            NightTheme();
        }
        else
        {
            DayTheme();
        }
    }

    public void NightTheme()
    {
        ThemeChangeButton.IsChecked = true;
        NavTopBar.Background = new SolidColorBrush(Color.Parse("#0F4C75"));
        mainTopGrid.Background = new SolidColorBrush(Color.Parse("#1E1E1E"));
        WINDOWWWWWWWW.Background = new SolidColorBrush(Color.Parse("#1E1E1E"));
        MainText.Background = new SolidColorBrush(Color.Parse("#1E1E1E"));
        MainText.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));

        sep1.Background = new SolidColorBrush(Color.Parse("#0fa6a8"));
        sep2.Background = new SolidColorBrush(Color.Parse("#0fa6a8"));

        Menu_file.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        Menu_edit.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        Menu_help.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));

        _1.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        _2.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        _3.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        _4.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        _5.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        _6.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        _7.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        _8.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        _9.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        _10.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        _11.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        _12.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        _13.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        _14.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        _15.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));

        TextWrapCheckBox.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        ThemeChangeButton.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));
        SplitButtonFontSize.Foreground = new SolidColorBrush(Color.Parse("#0fa6a8"));

        var focusEditorNight = new Style(x => x.OfType<TextBox>().Class(":focus"))
        {
            Setters = {
                new Setter(TextBox.BackgroundProperty, new SolidColorBrush(Color.Parse("#1E1E1E")))
            }
        };
        Application.Current?.Styles.Add(focusEditorNight);
    }
    public void DayTheme()
    {
        NavTopBar.Background = new SolidColorBrush(Color.Parse("#0fa6a8"));
        mainTopGrid.Background = new SolidColorBrush(Color.Parse("#D3D3D3"));
        WINDOWWWWWWWW.Background = new SolidColorBrush(Color.Parse("#ffffffff"));
        MainText.Background = new SolidColorBrush(Color.Parse("#ffffffff"));
        MainText.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));

        sep1.Background = new SolidColorBrush(Color.Parse("#FF000000"));
        sep2.Background = new SolidColorBrush(Color.Parse("#FF000000"));

        Menu_file.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        Menu_edit.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        Menu_help.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));

        _1.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        _2.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        _3.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        _4.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        _5.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        _6.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        _7.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        _8.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        _9.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        _10.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        _11.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        _12.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        _13.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        _14.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        _15.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));

        TextWrapCheckBox.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        ThemeChangeButton.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));
        SplitButtonFontSize.Foreground = new SolidColorBrush(Color.Parse("#FF000000"));

        var focusEditorDay = new Style(x => x.OfType<TextBox>().Class(":focus"))
        {
            Setters = {
                new Setter(TextBox.BackgroundProperty, new SolidColorBrush(Color.Parse("#FFFFFF")))
            }
        };
        Application.Current?.Styles.Add(focusEditorDay);
    }

    private void OnFontSizeClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button item)
        {
            if (int.TryParse(item.Content?.ToString(), out int newSize))
            {
                MainText.FontSize = newSize;
            }
        }
    }



    //second divider
    private void UndoMenu_Click(object? sender, RoutedEventArgs e)
    {

    }

    private void RedoMenu_Click(object? sender, RoutedEventArgs e)
    {

    }


    //third divider
    private void AboutMenu_Click(object? sender, RoutedEventArgs e)
    {
        var aboutWindow = new AboutWindow();
        aboutWindow.ShowDialog(this);
    }
    private void HotKeysMenu_Click(object? sender, RoutedEventArgs e)
    {
        var hotKeysWindow = new HotKeysWindow();
        hotKeysWindow.ShowDialog(this);
    }
}