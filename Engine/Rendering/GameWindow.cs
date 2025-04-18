using System;
using Microsoft.Xna.Framework;

namespace MonoEight;

/// <summary>
/// GameWindow class for managing the game window's size, scale, and fullscreen mode.
/// </summary>
public static class GameWindow
{
    private const int WIDTH = 256;
    private const int HEIGHT = 192;

    private const int MIN_SCALE = 1;
    private const int MAX_SCALE = 16;

    private static int _scale = 10;
    private static bool _isFullscreen = false;
    private static bool _startFullscreen = false;

    private static GraphicsDeviceManager _graphics;
    private static Microsoft.Xna.Framework.GameWindow _window;

    public static int Width => WIDTH;
    public static int Height => HEIGHT;
    public static Point Size => new(WIDTH, HEIGHT);

    /// <summary>
    /// Gets or sets the scale of the game window.
    /// </summary>
    public static int Scale
    {
        get => _scale;
        set
        {
            if (value < MIN_SCALE)
                _scale = MIN_SCALE;
            else if (value > MAX_SCALE)
                _scale = MAX_SCALE;
            else
                _scale = value;

            if (!_isFullscreen)
                UpdateWindowSize();
        }
    }

    /// <summary>
    /// Gets or sets whether the game window is in fullscreen mode.
    /// When set to true, the game window will be borderless and cover the entire screen.
    /// When set to false, the game window will be centered on the screen with a specific size.
    /// </summary>
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
            }
            else
            {
                Scale = CalculateScale();
                _graphics.PreferredBackBufferWidth = ScaledWidth;
                _graphics.PreferredBackBufferHeight = ScaledHeight;
                _window.IsBorderless = false;
                _window.Position = new Point
                (
                    (_graphics.GraphicsDevice.DisplayMode.Width - ScaledWidth) / 2,
                    (_graphics.GraphicsDevice.DisplayMode.Height - ScaledHeight) / 2
                );
            }

            _graphics.ApplyChanges();
        }
    }

    /// <summary>
    /// Gets or sets whether the game window should start in fullscreen mode.
    /// This property is used to determine the initial state of the game window when the game starts.
    /// </summary>
    public static bool StartFullscreen
    {
        get => _startFullscreen;
        set => _startFullscreen = value;
    }

    public static int ScaledWidth => WIDTH * _scale;
    public static int ScaledHeight => HEIGHT * _scale;
    public static Point ScaledSize => new(ScaledWidth, ScaledHeight);
    public static Vector2 Center => new(ScaledWidth / 2, ScaledHeight / 2);

    public static GraphicsDeviceManager Graphics => _graphics;
    public static Point DisplaySize => new(_graphics.GraphicsDevice.DisplayMode.Width, _graphics.GraphicsDevice.DisplayMode.Height);

    public static void Initialize(GraphicsDeviceManager graphics, Microsoft.Xna.Framework.GameWindow window)
    {
        _graphics = graphics;
        _window = window;

        if (StartFullscreen)
        {
            IsFullscreen = true;
            return;
        }

        Scale = CalculateScale();
        UpdateWindowSize();
    }

    public static void UpdateWindowSize()
    {
        if (_isFullscreen)
            return;

        _graphics.PreferredBackBufferWidth = ScaledWidth;
        _graphics.PreferredBackBufferHeight = ScaledHeight;
        _graphics.ApplyChanges();
    }

    public static void ToggleFullscreen()
    {
        IsFullscreen = !IsFullscreen;
    }

    private static int CalculateScale()
    {
        return (int)Math.Floor(_graphics.GraphicsDevice.DisplayMode.Height / (float)HEIGHT) - 1;
    }
}