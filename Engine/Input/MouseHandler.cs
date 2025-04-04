using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

/// <summary>
/// Handles mouse input for the game, including position, button states, and scroll wheel value.
/// Provides methods to check the state of left and right mouse buttons.
/// </summary>
public class MouseHandler
{
    private MouseState _currentMouseState;
    private MouseState _previousMouseState;

    public Point UnscaledPosition => _currentMouseState.Position;

    public Vector2 Position
    {
        get
        {
            Vector2 scaledPosition = UnscaledPosition.ToVector2();

            if (GameWindow.IsFullscreen)
            {
                Point displaySize = GameWindow.DisplaySize;
                float displayWidth = displaySize.X;
                float displayHeight = displaySize.Y;

                float gameAspectRatio = (float)GameWindow.Width / GameWindow.Height;
                float screenAspectRatio = displayWidth / displayHeight;

                float screenWidth, screenHeight;
                float offsetX = 0, offsetY = 0;

                if (screenAspectRatio >= gameAspectRatio)
                {
                    screenHeight = displayHeight;
                    screenWidth = screenHeight * gameAspectRatio;
                    offsetX = (displayWidth - screenWidth) / 2;
                }
                else
                {
                    screenWidth = displayWidth;
                    screenHeight = screenWidth / gameAspectRatio;
                    offsetY = (displayHeight - screenHeight) / 2;
                }

                scaledPosition.X = (scaledPosition.X - offsetX) * (GameWindow.Width * GameWindow.Scale) / screenWidth;
                scaledPosition.Y = (scaledPosition.Y - offsetY) * (GameWindow.Height * GameWindow.Scale) / screenHeight;
            }

            scaledPosition /= GameWindow.Scale;

            return scaledPosition + Camera.Position;
        }
    }

    public float X => Position.X;
    public float Y => Position.Y;

    public void Update()
    {
        _previousMouseState = _currentMouseState;
        _currentMouseState = Mouse.GetState();
    }

    public bool LeftButtonDown() => _currentMouseState.LeftButton == ButtonState.Pressed;
    public bool LeftButtonUp() => _currentMouseState.LeftButton == ButtonState.Released;
    public bool LeftButtonPressed() => _currentMouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released;
    public bool LeftButtonReleased() => _currentMouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed;

    public bool RightButtonDown() => _currentMouseState.RightButton == ButtonState.Pressed;
    public bool RightButtonUp() => _currentMouseState.RightButton == ButtonState.Released;
    public bool RightButtonPressed() => _currentMouseState.RightButton == ButtonState.Pressed && _previousMouseState.RightButton == ButtonState.Released;
    public bool RightButtonReleased() => _currentMouseState.RightButton == ButtonState.Released && _previousMouseState.RightButton == ButtonState.Pressed;

    public int ScrollWheelValue => _currentMouseState.ScrollWheelValue;
    public int ScrollWheelDelta => _currentMouseState.ScrollWheelValue - _previousMouseState.ScrollWheelValue;
}