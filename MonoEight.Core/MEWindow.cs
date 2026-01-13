using Microsoft.Xna.Framework;

namespace MonoEight.Core;

/// <summary>
/// A static manager for controlling the application window.
/// </summary>
public static class MEWindow
{
    private static bool _isFullscreen = false;

    /// <summary>
    /// Gets or sets the internal game resolution.
    /// </summary>
    /// <remarks>
    /// This is the size of the render target, not necessarily the size of the actual OS window.
    /// </remarks>
    public static Point Resolution { get; set; } = new Point(128, 96);
    
    /// <summary>
    /// Gets or sets whether the game should start in fullscreen mode.
    /// </summary>
    public static bool StartFullscreen { get; set; }

    /// <summary>
    /// Gets or sets whether the game window is currently in fullscreen mode.
    /// </summary>
    public static bool IsFullscreen
    {
        get => _isFullscreen;
        set => SetFullScreen(value);
    }

    /// <summary>
    /// Gets the <see cref="GraphicsDeviceManager"/> used by the game.
    /// </summary>
    public static GraphicsDeviceManager? Graphics { get; private set; }
    
    /// <summary>
    /// Gets the underlying <see cref="GameWindow"/>.
    /// </summary>
    public static GameWindow? Window { get; private set; }

    /// <summary>
    /// Initializes the window manager with the required MonoGame references.
    /// </summary>
    /// <param name="graphics">The main graphics device manager.</param>
    /// <param name="window">The main game window.</param>
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

    /// <summary>
    /// Toggles the window between borderless fullscreen and windowed mode.
    /// </summary>
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
