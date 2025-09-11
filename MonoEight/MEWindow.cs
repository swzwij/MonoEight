using System;
using Microsoft.Xna.Framework;

namespace MonoEight;

public static class MEWindow
{
    private const int WIDTH = 64;
    private const int HEIGHT = 64;

    private static int _scale = 10;
    private static bool _isFullscreen = false;
    private static bool _startFullscreen = false;

    private static GraphicsDeviceManager _graphics;
    private static GameWindow _window;

    public static int Width => WIDTH;
    public static int Height => HEIGHT;
    public static Point Size => new(WIDTH, HEIGHT);

    public static int Scale
    {
        get => _scale;
        set
        {
            if (_isFullscreen)
                return;

            _scale = Math.Max(1, value);
            UpdateWindowSize();
        }
    }

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