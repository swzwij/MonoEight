using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class MouseHandler
{
    public bool IsEnabled { get; set; } = true;

    private MouseState _mouse;
    private MouseState _lastMouse;

    private Point _truePosition = new();
    private Vector2 _position = new();

    public Point TruePosition
    {
        get
        {
            Rectangle displayRect = GraphicsHelper.CalculateDisplayRect(MEWindow.Graphics.GraphicsDevice);

            int mouseX = _mouse.X;
            int mouseY = _mouse.Y;

            if (!displayRect.Contains(mouseX, mouseY))
                return _truePosition;

            float relativeX = (mouseX - displayRect.X) / (float)displayRect.Width;
            float relativeY = (mouseY - displayRect.Y) / (float)displayRect.Height;

            int gameX = (int)(relativeX * MEWindow.Resolution.X);
            int gameY = (int)(relativeY * MEWindow.Resolution.Y);

            _truePosition = new Point(gameX, gameY);
            return _truePosition;
        }
    }


    public Vector2 Position
    {
        get
        {
            Point screenPos = TruePosition;

            if (screenPos.X < 0 || screenPos.Y < 0)
                return _position;

            if (SceneManager.ActiveScene != null)
            {
                Vector2 resolutionOffset = new(MEWindow.Resolution.X / 2, MEWindow.Resolution.Y / 2);
                Vector2 cameraOffset = SceneManager.ActiveScene.Camera.Position - resolutionOffset;

                _position = new Vector2(screenPos.X, screenPos.Y) + cameraOffset;
                return _position;
            }

            _position = new(screenPos.X, screenPos.Y);
            return _position;
        }
    }

    public bool LeftDown => _mouse.LeftButton == ButtonState.Pressed;
    public bool LeftUp => _mouse.LeftButton == ButtonState.Released;
    public bool LeftPressed => _mouse.LeftButton == ButtonState.Pressed && _lastMouse.LeftButton == ButtonState.Released;
    public bool LeftReleased => _mouse.LeftButton == ButtonState.Released && _lastMouse.LeftButton == ButtonState.Pressed;

    public bool RightDown => _mouse.RightButton == ButtonState.Pressed;
    public bool RightUp => _mouse.RightButton == ButtonState.Released;
    public bool RightPressed => _mouse.RightButton == ButtonState.Pressed && _lastMouse.RightButton == ButtonState.Released;
    public bool RightReleased => _mouse.RightButton == ButtonState.Released && _lastMouse.RightButton == ButtonState.Pressed;

    public void Update()
    {
        if (!IsEnabled)
            return;

        _lastMouse = _mouse;
        _mouse = Mouse.GetState();

        _ = TruePosition;
        _ = Position;
        
        // TODO
    }
}
