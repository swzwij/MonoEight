using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoEight.Core.Scenes;

namespace MonoEight.Core.UserInput;

public class MouseHandler
{
    public bool IsEnabled { get; set; } = true;

    private MouseState _mouse;
    private MouseState _lastMouse;
    
    private Rectangle _displayRect;

    public Point TruePosition {get; private set;}
    public Vector2 Position { get; private set; }

    public bool LeftDown => _mouse.LeftButton == ButtonState.Pressed;
    public bool LeftUp => _mouse.LeftButton == ButtonState.Released;
    public bool LeftPressed => _mouse.LeftButton == ButtonState.Pressed && _lastMouse.LeftButton == ButtonState.Released;
    public bool LeftReleased => _mouse.LeftButton == ButtonState.Released && _lastMouse.LeftButton == ButtonState.Pressed;

    public bool RightDown => _mouse.RightButton == ButtonState.Pressed;
    public bool RightUp => _mouse.RightButton == ButtonState.Released;
    public bool RightPressed => _mouse.RightButton == ButtonState.Pressed && _lastMouse.RightButton == ButtonState.Released;
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
