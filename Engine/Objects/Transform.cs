using Microsoft.Xna.Framework;

namespace MonoEight;

public class Transform
{
    public Vector2 Position;
    public float Rotation;

    public Transform(Vector2 position, float rotation)
    {
        Position = position;
        Rotation = rotation;
    }

    public Transform(Vector2 position)
    {
        Position = position;
        Rotation = 0;
    }

    public Transform()
    {
        Position = Vector2.Zero;
        Rotation = 0;
    }
}
