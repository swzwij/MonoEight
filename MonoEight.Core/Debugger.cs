using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight.Core;

/// <summary>
/// Provides static methods for drawing basic debug shapes, such as pixels, lines, and squares.
/// </summary>
public static class Debugger
{
    private static Texture2D _pixel = null!;

    /// <summary>
    /// Initializes the debugging tools by creating a 1x1 white texture used for drawing shapes.
    /// </summary>
    /// <param name="graphics"><see cref="GraphicsDeviceManager"/></param>
    public static void Initialize(GraphicsDeviceManager graphics)
    {
        _pixel = new Texture2D(graphics.GraphicsDevice, 1, 1);
        _pixel.SetData([Color.White]);
    }

    /// <summary>
    /// Draws a single pixel at the given position.
    /// </summary>
    /// <param name="spriteBatch"><see cref="SpriteBatch"/></param>
    /// <param name="position">The position of the pixel.</param>
    /// <param name="color">The color of the pixel.</param>
    public static void DrawPixel(SpriteBatch spriteBatch, Point position, Color color)
    {
        spriteBatch.Draw(_pixel, position.ToVector2(), color);
    }

    /// <summary>
    /// Draws a line between two given points.
    /// The line is 1 pixel wide. 
    /// </summary>
    /// <param name="spriteBatch"><see cref="SpriteBatch"/></param>
    /// <param name="start">The start point of the line.</param>
    /// <param name="end">The end point of the line.</param>
    /// <param name="color">The color of the line.</param>
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

    /// <summary>
    /// Draws a wireframe square centered on the given position.
    /// The width of the wireframe is 1 pixel. 
    /// </summary>
    /// <param name="spriteBatch"><see cref="SpriteBatch"/></param>
    /// <param name="position">The position of the square.</param>
    /// <param name="size">The size of the square.</param>
    /// <param name="color">The color of the square.</param>
    public static void DrawSquare(SpriteBatch spriteBatch, Point position, Point size, Color color)
    {
        position -= (size.ToVector2() / 2).ToPoint();
        DrawLine(spriteBatch, position, new Point(position.X + size.X, position.Y), color);
        DrawLine(spriteBatch, new Point(position.X + size.X, position.Y), position + size, color);
        DrawLine(spriteBatch, position + size, new Point(position.X, position.Y + size.Y), color);
        DrawLine(spriteBatch, new Point(position.X, position.Y + size.Y), position, color);
    }

    /// <summary>
    /// Draws a wireframe circle centered on the given position.
    /// The width of the wireframe is 1 pixel.
    /// </summary>
    /// <param name="spriteBatch"><see cref="SpriteBatch"/></param>
    /// <param name="position">The position of the circle</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="color">The color of the circle.</param>
    public static void DrawCircle(SpriteBatch spriteBatch, Point position, int radius, Color color)
    {
        const int segments = 36;
        const float increment = MathHelper.TwoPi / segments;
        float theta = 0f;
        Point lastPoint = new(position.X + radius, position.Y);
        for (int i = 1; i <= segments; i++)
        {
            theta += increment;
            Point nextPoint = new
            (
                position.X + (int)(radius * Math.Cos(theta)),
                position.Y + (int)(radius * Math.Sin(theta))
            );
            DrawLine(spriteBatch, lastPoint, nextPoint, color);
            lastPoint = nextPoint;
        }
    }
}
