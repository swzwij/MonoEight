using Microsoft.Xna.Framework;

namespace MonoEight;

public static class MEWindow
{
    private const int WIDTH = 96;
    private const int HEIGHT = 64;

    private static bool _isFullscreen = false;
    private static bool _startFullscreen = false;

    private static GraphicsDeviceManager _graphics;
    private static GameWindow _window;

    public static int Width => WIDTH;
    public static int Height => HEIGHT;
    public static Point Size => new(WIDTH, HEIGHT);

    public static bool IsFullscreen
    {
        get => _isFullscreen;
        set
        {
            if (_isFullscreen == value)
                return;

            _isFullscreen = value;

            if (_isFullscreen)
            {
                _graphics.PreferredBackBufferWidth = _graphics.GraphicsDevice.DisplayMode.Width;
                _graphics.PreferredBackBufferHeight = _graphics.GraphicsDevice.DisplayMode.Height;
                _window.IsBorderless = true;
                _window.Position = new Point(0, 0);
                _window.AllowUserResizing = false;
            }
            else
            {
                _graphics.PreferredBackBufferWidth = OptimalSize.X;
                _graphics.PreferredBackBufferHeight = OptimalSize.Y;
                _window.IsBorderless = false;
                _window.AllowUserResizing = true;

                _window.Position = new Point
                (
                    (_graphics.GraphicsDevice.DisplayMode.Width - OptimalSize.X) / 2,
                    (_graphics.GraphicsDevice.DisplayMode.Height - OptimalSize.Y) / 2
                );
            }

            _graphics.ApplyChanges();
        }
    }

    public static bool StartFullscreen
    {
        get => _startFullscreen;
        set => _startFullscreen = value;
    }

    public static int WindowWidth => _graphics.PreferredBackBufferWidth;
    public static int WindowHeight => _graphics.PreferredBackBufferHeight;
    public static Point WindowSize => new(WindowWidth, WindowHeight);
    public static Vector2 WindowCenter => new(WindowWidth / 2, WindowHeight / 2);

    private static Point OptimalSize
    {
        get
        {
            int targetHeight = _graphics.GraphicsDevice.DisplayMode.Height * 3 / 4;
            float aspectRatio = (float)WIDTH / (float)HEIGHT;
            int targetWidth = (int)(targetHeight * aspectRatio);
            return new(targetWidth, targetHeight);
        }
    }

    public static GraphicsDeviceManager Graphics => _graphics;
    public static Point DisplaySize => new(_graphics.GraphicsDevice.DisplayMode.Width, _graphics.GraphicsDevice.DisplayMode.Height);

    public static void Initialize(GraphicsDeviceManager graphics, GameWindow window)
    {
        _graphics = graphics;
        _window = window;

        if (StartFullscreen)
        {
            IsFullscreen = true;
            return;
        }

        _graphics.PreferredBackBufferWidth = OptimalSize.X;
        _graphics.PreferredBackBufferHeight = OptimalSize.Y;
        _window.AllowUserResizing = true;
        _graphics.ApplyChanges();
    }

    public static void ToggleFullscreen()
    {
        IsFullscreen = !IsFullscreen;
    }
}