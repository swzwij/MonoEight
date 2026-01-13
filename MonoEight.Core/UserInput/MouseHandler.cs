using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoEight.Core.Scenes;

namespace MonoEight.Core.UserInput;

/// <summary>
/// Manages the mouse button input and cursor positioning. 
/// </summary>
public class MouseHandler
{
    private MouseState _mouse;
    private MouseState _lastMouse;
    
    private Rectangle _displayRect;
    
    /// <summary>
    /// Gets or sets whether mouse input processing is enabled.
    /// Default is <c>true</c>.
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Gets the mouse cursor position in screen coordinates.
    /// </summary>
    /// <remarks>
    /// The value is scaled relative to the internal resolution (<see cref="MEWindow.Resolution"/>), ignoring the camera.
    /// </remarks>
    public Point TruePosition {get; private set;}

    /// <summary>
    /// Gets the mouse cursor position in world coordinates.
    /// </summary>
    /// <remarks>
    /// This value includes the camera offset.
    /// </remarks>
    public Vector2 Position { get; private set; }

    /// <summary>
    /// Gets whether the left mouse button is currently held down.
    /// </summary>
    public bool LeftDown => _mouse.LeftButton == ButtonState.Pressed;

    /// <summary>
    /// Gets whether the left mouse button is currently up.
    /// </summary>
    public bool LeftUp => _mouse.LeftButton == ButtonState.Released;

    /// <summary>
    /// Gets whether the left mouse button was pressed this frame.
    /// </summary>
    public bool LeftPressed => _mouse.LeftButton == ButtonState.Pressed && _lastMouse.LeftButton == ButtonState.Released;

    /// <summary>
    /// Gets whether the left mouse button was released this frame.
    /// </summary>
    public bool LeftReleased => _mouse.LeftButton == ButtonState.Released && _lastMouse.LeftButton == ButtonState.Pressed;

    /// <summary>
    /// Gets whether the right mouse button is currently held down.
    /// </summary>
    public bool RightDown => _mouse.RightButton == ButtonState.Pressed;

    /// <summary>
    /// Gets whether the right mouse button is currently up.
    /// </summary>
    public bool RightUp => _mouse.RightButton == ButtonState.Released;

    /// <summary>
    /// Gets whether the right mouse button was pressed this frame.
    /// </summary>
    public bool RightPressed => _mouse.RightButton == ButtonState.Pressed && _lastMouse.RightButton == ButtonState.Released;

    /// <summary>
    /// Gets whether the right mouse button was released this frame.
    /// </summary>
    public bool RightReleased => _mouse.RightButton == ButtonState.Released && _lastMouse.RightButton == ButtonState.Pressed;

    public void Update(Rectangle displayRect)
    {
        if (!IsEnabled)
            return;

        _displayRect = displayRect;
        
        _lastMouse = _mouse;
        _mouse = Mouse.GetState();

        UpdateTruePosition();
        UpdatePosition();
    }
    
    private void UpdateTruePosition()
    {
        int mouseX = _mouse.X;
        int mouseY = _mouse.Y;

        if (!_displayRect.Contains(mouseX, mouseY))
            return;

        float relativeX = (mouseX - _displayRect.X) / (float)_displayRect.Width;
        float relativeY = (mouseY - _displayRect.Y) / (float)_displayRect.Height;

        int gameX = (int)(relativeX * MEWindow.Resolution.X);
        int gameY = (int)(relativeY * MEWindow.Resolution.Y);

        TruePosition = new Point(gameX, gameY);
    }

    private void UpdatePosition()
    {
        if (TruePosition.X < 0 || TruePosition.Y < 0)
            return;

        Vector2 resolutionOffset = new(MEWindow.Resolution.X / 2, MEWindow.Resolution.Y / 2);
        Vector2 cameraOffset = SceneManager.ActiveScene.Camera.Position - resolutionOffset;

        Position = new Vector2(TruePosition.X, TruePosition.Y) + cameraOffset;
    }
}
