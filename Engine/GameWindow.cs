using System;
using Microsoft.Xna.Framework;

namespace MonoEight;

public static class GameWindow
{
    private const int WIDTH = 144;
    private const int HEIGHT = 128;

    private const int MIN_SCALE = 1;
    private const int MAX_SCALE = 16;

    private static int _scale = 10;

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

            UpdateWindowSize();
        }
    }

    public static int ScaledWidth => WIDTH * _scale;
    public static int ScaledHeight => HEIGHT * _scale;
    public static Point ScaledSize => new(ScaledWidth, ScaledHeight);
    public static Vector2 Center => new(ScaledWidth / 2, ScaledHeight / 2);

    public static void Initialize(GraphicsDeviceManager graphics)
    {
        _graphics = graphics;

        Scale = (int)Math.Floor(graphics.GraphicsDevice.DisplayMode.Height / (float)HEIGHT) - 1;
    }

    public static void UpdateWindowSize()
    {
        _graphics.PreferredBackBufferWidth = ScaledWidth;
        _graphics.PreferredBackBufferHeight = ScaledHeight;
        _graphics.ApplyChanges();
    }
}