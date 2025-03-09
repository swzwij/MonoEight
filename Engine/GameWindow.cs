using Microsoft.Xna.Framework;

namespace MonoEight;

public static class GameWindow
{
    private const int WIDTH = 144;
    private const int HEIGHT = 128;

    private const int MIN_SCALE = 1;
    private const int MAX_SCALE = 16;

    private static int _scale = 4;

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

    public static void Initialize(GraphicsDeviceManager graphics)
    {
        _graphics = graphics;
    }

    public static void UpdateWindowSize()
    {
        _graphics.PreferredBackBufferWidth = ScaledWidth;
        _graphics.PreferredBackBufferHeight = ScaledHeight;
        _graphics.ApplyChanges();
    }
}