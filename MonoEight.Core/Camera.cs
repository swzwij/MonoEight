using Microsoft.Xna.Framework;

namespace MonoEight.Core;

/// <summary>
/// Represents a 2D camera that manages view transformations and background color for rendering scenes.
/// </summary>
/// <remarks>
/// The Camera class provides functionality to control the visible area of a scene by adjusting its
/// position and transformation matrix. The camera's position is relative to the center of the window resolution, allowing for intuitive
/// placement within the scene. The background color determines the color used to clear the viewport before rendering objects.
/// </remarks>
public class Camera
{
    private Matrix _transform;
    private Vector2 _position;

    /// <summary>
    /// Gets or sets the background color.
    /// </summary>
    public Color BackgroundColor { get; set; }

    /// <summary>
    /// Gets the transformation matrix that defines the spatial relationship of the object in world coordinates.
    /// </summary>
    public Matrix Transform => _transform;

    /// <summary>
    /// Gets or sets the position of the camera in world coordinates.
    /// </summary>
    /// <remarks>
    /// The position is adjusted to be relative to the center of the window resolution.
    /// </remarks>
    public Vector2 Position
    {
        get => _position + new Vector2(MEWindow.Resolution.X / 2 , MEWindow.Resolution.Y / 2);
        set
        {
            _position = new(value.X - MEWindow.Resolution.X / 2, value.Y - MEWindow.Resolution.Y / 2);
            UpdatePosition();
        }
    }

    /// <summary>
    /// Initializes a new instance of the Camera class with the position set to (0,0) and the background color set
    /// to blue.
    /// </summary>
    public Camera()
    {
        Position = Vector2.Zero;
        BackgroundColor = MEColors.Blue;
    }

    private void UpdatePosition()
    {
        _transform = Matrix.CreateTranslation((int)-_position.X, (int)-_position.Y, 0);
    }
}
