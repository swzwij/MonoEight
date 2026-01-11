using Microsoft.Xna.Framework;

namespace MonoEight.Core;

public static class VectorPointExtensions
{
    public static Point Int(this Vector2 vector)
    {
        return new Point((int)vector.X, (int)vector.Y);
    }
    
    public static Vector2 Float(this Point vector)
    {
        return new Vector2(vector.X, vector.Y);
    }
    
    public static Vector2 Cast(this Vector2 vector)
    {
        return vector.Int().Float();
    }
}
