using Microsoft.Xna.Framework;

namespace MonoEight.Core;

/// <summary>
/// Provides static methods and properties for managing the main game window, including resolution, fullscreen mode, and
/// access to the underlying graphics and window objects.
/// </summary>
public static class MEWindow
{
    private static bool _isFullscreen = false;

    public static Point Resolution { get; set; } = new Point(128, 96);
    public static bool StartFullscreen { get; set; }

    private static bool IsFullscreen
    {
        get => _isFullscreen;
        set => SetFullScreen(value);
    }

    public static GraphicsDeviceManager? Graphics { get; private set; }
    public static GameWindow? Window { get; private set; }

    public static void Initialize(GraphicsDeviceManager graphics, GameWindow window)
    {
        Graphics = graphics;
        Window = window;

        if (StartFullscreen)
        {
            IsFullscreen = true;
            return;
        }

        Windowed();
    }

    public static void ToggleFullscreen()
    {
        IsFullscreen = !IsFullscreen;
    }

    private static void SetFullScreen(bool value)
    {
        if (_isFullscreen == value)
            return;

        _isFullscreen = value;

        if (_isFullscreen)
            FullScreen();
        else
            Windowed();
    }

    private static void FullScreen()
    {
        Graphics!.PreferredBackBufferWidth = Graphics.GraphicsDevice.DisplayMode.Width;
        Graphics.PreferredBackBufferHeight = Graphics.GraphicsDevice.DisplayMode.Height;

        Window!.IsBorderless = true;
        Window.Position = new Point(0, 0);
        Window.AllowUserResizing = false;

        Graphics.ApplyChanges();
    }

    private static void Windowed()
    {
        Point windowedSize = CalculateWindowedSize();

        Graphics!.PreferredBackBufferWidth = windowedSize.X;
        Graphics.PreferredBackBufferHeight = windowedSize.Y;

        Window!.IsBorderless = false;
        Window.AllowUserResizing = true;

        Window.Position = new Point
        (
            (Graphics.GraphicsDevice.DisplayMode.Width - windowedSize.X) / 2,
            (Graphics.GraphicsDevice.DisplayMode.Height - windowedSize.Y) / 2
        );

        Graphics.ApplyChanges();
    }

    private static Point CalculateWindowedSize()
    {
        int screenHeight = Graphics!.GraphicsDevice.DisplayMode.Height;
        int windowHeight = screenHeight * 3 / 4;

        float aspectRatio = (float)Resolution.X / Resolution.Y;
        int windowWidth = (int)(windowHeight * aspectRatio);

        return new Point(windowWidth, windowHeight);
    }
}
