using Microsoft.Xna.Framework;

namespace MonoEight;

/// <summary>
/// Provides static methods and properties for managing the main game window, including resolution, fullscreen mode, and
/// access to the underlying graphics and window objects.
/// </summary>
public static class MEWindow
{
    private static GraphicsDeviceManager _graphics;
    private static GameWindow _window;

    private static bool _isFullscreen = false;

    public static Point Resolution { get; set; } = new Point(128, 96);
    public static bool StartFullscreen { get; set; }

    public static bool IsFullscreen
    {
        get => _isFullscreen;
        set => SetFullScreen(value);
    }

    public static GraphicsDeviceManager Graphics => _graphics;
    public static GameWindow Window => _window;

    public static void Initialize(GraphicsDeviceManager graphics, GameWindow window)
    {
        _graphics = graphics;
        _window = window;

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
        _graphics.PreferredBackBufferWidth = _graphics.GraphicsDevice.DisplayMode.Width;
        _graphics.PreferredBackBufferHeight = _graphics.GraphicsDevice.DisplayMode.Height;

        _window.IsBorderless = true;
        _window.Position = new Point(0, 0);
        _window.AllowUserResizing = false;

        _graphics.ApplyChanges();
    }

    private static void Windowed()
    {
        Point windowedSize = CalculateWindowedSize();

        _graphics.PreferredBackBufferWidth = windowedSize.X;
        _graphics.PreferredBackBufferHeight = windowedSize.Y;

        _window.IsBorderless = false;
        _window.AllowUserResizing = true;

        _window.Position = new Point
        (
            (_graphics.GraphicsDevice.DisplayMode.Width - windowedSize.X) / 2,
            (_graphics.GraphicsDevice.DisplayMode.Height - windowedSize.Y) / 2
        );

        _graphics.ApplyChanges();
    }

    private static Point CalculateWindowedSize()
    {
        int screenHeight = _graphics.GraphicsDevice.DisplayMode.Height;
        int windowHeight = screenHeight * 3 / 4;

        float aspectRatio = (float)Resolution.X / (float)Resolution.Y;
        int windowWidth = (int)(windowHeight * aspectRatio);

        return new(windowWidth, windowHeight);
    }
}