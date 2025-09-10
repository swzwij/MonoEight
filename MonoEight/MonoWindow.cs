using System;
using Microsoft.Xna.Framework;

namespace MonoEight;

public static class MonoWindow
{
    private static Point _size;
    private static int _scale = 10;
    private static bool _isFullscreen;

    private static GraphicsDeviceManager _graphics;
    private static GameWindow _window;

    public static bool StartFullscreen { get; set; } = false;

    public static bool IsFullscreen
    {
        get => _isFullscreen;
        set => FullScreen(value);
    }

    public static int Scale
    {
        get => _scale;
        set
        {
            _scale = value;
            UpdateWindowSize();
        }
    }

    // Base dimensions (unscaled)
    public static int Width => _size.X;
    public static int Height => _size.Y;
    public static Point BaseSize => _size;

    // Scaled dimensions
    public static Point Size => new(_size.X * _scale, _size.Y * _scale);
    public static int ScaledWidth => _size.X * _scale;
    public static int ScaledHeight => _size.Y * _scale;

    public static void Initialize(GraphicsDeviceManager graphics, GameWindow window)
    {
        _graphics = graphics;
        _window = window;
    }

    public static void Create(int xSize, int ySize)
    {
        _size = new(xSize, ySize);

        if (StartFullscreen)
        {
            IsFullscreen = true;
            return;
        }

        Scale = CalculateScale();
        UpdateWindowSize();
    }

    public static void FullScreen(bool value)
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
            _scale = CalculateScale();
            _graphics.PreferredBackBufferWidth = _size.X * _scale;
            _graphics.PreferredBackBufferHeight = _size.Y * _scale;
            _window.IsBorderless = false;
            _window.Position = new Point
            (
                (_graphics.GraphicsDevice.DisplayMode.Width - _size.X * _scale) / 2,
                (_graphics.GraphicsDevice.DisplayMode.Height - _size.Y * _scale) / 2
            );
        }

        _graphics.ApplyChanges();
    }

    public static void UpdateWindowSize()
    {
        if (_isFullscreen)
            return;

        _graphics.PreferredBackBufferWidth = _size.X * _scale;
        _graphics.PreferredBackBufferHeight = _size.Y * _scale;
        _graphics.ApplyChanges();
    }

    public static void ToggleFullscreen()
    {
        IsFullscreen = !IsFullscreen;
    }

    private static int CalculateScale()
    {
        return (int)Math.Floor(_graphics.GraphicsDevice.DisplayMode.Height / (float)_size.Y) - 1;
    }
}