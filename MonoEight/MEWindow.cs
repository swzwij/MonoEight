using Microsoft.Xna.Framework;

namespace MonoEight;

public static class MEWindow
{
    private static GraphicsDeviceManager _graphics;
    private static GameWindow _window;

    private static bool _isFullscreen = false;

    public static int Width { get; set; }
    public static int Height { get; set; }
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
        Point optimalSize = CalculateOptimalSize();

        _graphics.PreferredBackBufferWidth = optimalSize.X;
        _graphics.PreferredBackBufferHeight = optimalSize.Y;

        _window.IsBorderless = false;
        _window.AllowUserResizing = true;

        _window.Position = new Point
        (
            (_graphics.GraphicsDevice.DisplayMode.Width - optimalSize.X) / 2,
            (_graphics.GraphicsDevice.DisplayMode.Height - optimalSize.Y) / 2
        );

        _graphics.ApplyChanges();
    }

    private static Point CalculateOptimalSize()
    {
        int targetHeight = _graphics.GraphicsDevice.DisplayMode.Height * 3 / 4;
        float aspectRatio = (float)Width / (float)Height;
        int targetWidth = (int)(targetHeight * aspectRatio);
        return new(targetWidth, targetHeight);
    }
}