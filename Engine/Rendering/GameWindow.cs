using System;
using Microsoft.Xna.Framework;

namespace MonoEight;

public static class GameWindow
{
    // private const int WIDTH = 144;
    // private const int HEIGHT = 128;

    private const int WIDTH = 256;
    private const int HEIGHT = 192;

    private const int MIN_SCALE = 1;
    private const int MAX_SCALE = 16;

    private static int _scale = 10;
    private static bool _isFullscreen = false;

    private static GraphicsDeviceManager _graphics;

    public static int Width => WIDTH;
    public static int Height => HEIGHT;
    public static Point Size => new(WIDTH, HEIGHT);

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
                _graphics.IsFullScreen = true;
            }
            else
            {
                Scale = (int)Math.Floor(_graphics.GraphicsDevice.DisplayMode.Height / (float)HEIGHT);
                _graphics.PreferredBackBufferWidth = ScaledWidth;
                _graphics.PreferredBackBufferHeight = ScaledHeight;
                _graphics.IsFullScreen = false;
            }

            _graphics.ApplyChanges();
        }
    }

    public static int ScaledWidth => WIDTH * _scale;
    public static int ScaledHeight => HEIGHT * _scale;
    public static Point ScaledSize => new(ScaledWidth, ScaledHeight);
    public static Vector2 Center => new(ScaledWidth / 2, ScaledHeight / 2);

    public static void Initialize(GraphicsDeviceManager graphics, bool startFullscreen = false)
    {
        _graphics = graphics;

        if (startFullscreen)
        {
            IsFullscreen = true;
            return;
        }

        Scale = (int)Math.Floor(graphics.GraphicsDevice.DisplayMode.Height / (float)HEIGHT);
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
}