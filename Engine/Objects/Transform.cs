using Microsoft.Xna.Framework;

namespace MonoEight;

/// <summary>
/// Represents a 2D transform with position and rotation properties.
/// </summary>
public class Transform
{
    public Point Position;
    public float Rotation;

    public Transform(Point position, float rotation)
    {
        Position = position;
        Rotation = rotation;
    }

    public Transform(Point position)
    {
        Position = position;
        Rotation = 0;
    }

    public Transform()
    {
        Position = Point.Zero;
        Rotation = 0;
    }
}
