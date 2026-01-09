using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight.Core;

/// <summary>
/// Provides static methods for drawing basic debug shapes, such as pixels, lines, and squares, using a
/// SpriteBatch <see cref="SpriteBatch"/>.
/// </summary>
public static class Debugger
{
    private static Texture2D _pixel;

    public static void Initialize(GraphicsDeviceManager graphics)
    {
        _pixel = new Texture2D(graphics.GraphicsDevice, 1, 1);
        _pixel.SetData([Color.White]);
    }

    public static void DrawPixel(SpriteBatch spriteBatch, Point position, Color color)
    {
        spriteBatch.Draw(_pixel, position.ToVector2(), color);
    }

    public static void DrawLine(SpriteBatch spriteBatch, Point start, Point end, Color color)
    {
        Point edge = end - start;
        double angle = Math.Atan2(edge.Y, edge.X);
        spriteBatch.Draw
        (
            _pixel,
            start.ToVector2(),
            null,
            color,
            (float)angle,
            Vector2.Zero,
            new Vector2(edge.ToVector2().Length(), 1),
            SpriteEffects.None,
            0
        );
    }

    public static void DrawSquare(SpriteBatch spriteBatch, Point position, Point size, Color color)
    {
        position -= (size.ToVector2() / 2).ToPoint();
        DrawLine(spriteBatch, position, new(position.X + size.X, position.Y), color);
        DrawLine(spriteBatch, new(position.X + size.X, position.Y), position + size, color);
        DrawLine(spriteBatch, position + size, new(position.X, position.Y + size.Y), color);
        DrawLine(spriteBatch, new(position.X, position.Y + size.Y), position, color);
    }

    public static void DrawCircle(SpriteBatch spriteBatch, Point center, int radius, Color color)
    {
        const int segments = 36;
        const float increment = MathHelper.TwoPi / segments;
        float theta = 0f;
        Point lastPoint = new(center.X + radius, center.Y);
        for (int i = 1; i <= segments; i++)
        {
            theta += increment;
            Point nextPoint = new
            (
                center.X + (int)(radius * Math.Cos(theta)),
                center.Y + (int)(radius * Math.Sin(theta))
            );
            DrawLine(spriteBatch, lastPoint, nextPoint, color);
            lastPoint = nextPoint;
        }
    }
}
