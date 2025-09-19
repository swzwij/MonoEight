using Microsoft.Xna.Framework;

namespace MonoEight;

public static class VectorPointExtenseions
{
    public static Point Int(this Vector2 vector)
    {
        return new((int)vector.X, (int)vector.Y);
    }

    public static Vector2 Float(this Point vector)
    {
        return new(vector.X, vector.Y);
    }
}