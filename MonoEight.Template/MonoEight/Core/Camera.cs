using Microsoft.Xna.Framework;

namespace MonoEight.Core;

/// <summary>
/// Represents a 2D camera that defines the viewable area of the game world.
/// </summary>
/// <remarks>
/// The camera centers the view on a given world position.
/// </remarks>
public class Camera
{
    private Vector2 _position;
    
    /// <summary>
    /// Gets or sets the background color used when clearing the screen.
    /// Default is <see cref="MEColors.Blue"/>.
    /// </summary>
    public Color BackgroundColor { get; set; }
    
    /// <summary>
    /// Gets the transformation matrix used for rendering.
    /// </summary>
    public Matrix Transform { get; private set; }

    /// <summary>
    /// Gets or sets the world position that the camera is at.
    /// </summary>
    /// <remarks>
    /// Setting this value automatically recalculates the <see cref="Transform"/> matrix.
    /// </remarks>
    public Vector2 Position
    {
        get => _position + new Vector2((int)(MEWindow.Resolution.X / 2f), (int)(MEWindow.Resolution.Y / 2f));
        set
        {
            _position = new Vector2
            (
                value.X - (int)(MEWindow.Resolution.X / 2f), 
                value.Y - (int)(MEWindow.Resolution.Y / 2f)
            );
            UpdatePosition();
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Camera"/> class centered at (0,0).
    /// </summary>
    public Camera()
    {
        Position = Vector2.Zero;
        BackgroundColor = MEColors.Blue;
    }

    private void UpdatePosition()
    {
        Transform = Matrix.CreateTranslation((int)-_position.X, (int)-_position.Y, 0);
    }
}
