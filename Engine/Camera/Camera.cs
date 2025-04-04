using Microsoft.Xna.Framework;

namespace MonoEight;

/// <summary>
/// Camera class for handling the camera's position and transformation matrix.
/// </summary>
public class Camera
{
    private static Matrix _transform;
    private static Vector2 _position;
    private static Color _backgroundColor;

    public static Matrix Transform => _transform;

    /// <summary>
    /// Gets or sets the camera's position in the world.
    /// </summary>
    public static Vector2 Position
    {
        get => _position;
        set
        {
            _position = value;
            UpdatePosition();
        }
    }

    /// <summary>
    /// Gets or sets the camera's position relative to the game window.
    /// The given position is relative to the center of the game window.
    /// </summary>
    public static Vector2 RelativePosition
    {
        get => new(_position.X + GameWindow.Width / 2, _position.Y + GameWindow.Height / 2);
        set
        {
            _position = new(value.X - GameWindow.Width / 2, value.Y - GameWindow.Height / 2);
            UpdatePosition();
        }
    }
    public static Color BackgroundColor
    {
        get => _backgroundColor;
        set => _backgroundColor = value;
    }

    public static void Initialize()
    {
        Position = Vector2.Zero;
        BackgroundColor = Color.Black;
        UpdatePosition();
    }

    private static void UpdatePosition()
    {
        _transform = Matrix.CreateTranslation(-_position.X, -_position.Y, 0);
    }
}