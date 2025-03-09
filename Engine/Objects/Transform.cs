using Microsoft.Xna.Framework;

namespace MonoEight;

public class Transform
{
    public Vector2 Position;
    public float Rotation;
    public float Scale;

    public Transform(Vector2 position, float rotation, float scale)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }

    public Transform(Vector2 position, float rotation)
    {
        Position = position;
        Rotation = rotation;
        Scale = 1;
    }

    public Transform(Vector2 position)
    {
        Position = position;
        Rotation = 0;
        Scale = 1;
    }

    public Transform()
    {
        Position = Vector2.Zero;
        Rotation = 0;
        Scale = 1;
    }
}
