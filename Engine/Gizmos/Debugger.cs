using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

/// <summary>
/// A static class for debugging purposes, providing methods to draw shapes and lines on the screen.
/// </summary>
public static class Debugger
{
    /// <summary>
    /// Creates a 1x1 pixel texture for drawing.
    /// </summary>
    /// <param name="graphicsDevice">The graphics device used to create the texture.</param>
    /// <returns>A 1x1 pixel texture.</returns>
    public static Texture2D CreatePixelTexture(GraphicsDevice graphicsDevice)
    {
        Texture2D Pixel = new(graphicsDevice, 1, 1);
        Pixel.SetData([Color.White]);
        return Pixel;
    }

    /// <summary>
    /// Draws a pixel at the specified position with the given color.
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used for drawing.</param>
    /// <param name="position">The position to draw the pixel at.</param>
    /// <param name="color">The color of the pixel.</param>
    public static void DrawPixel(SpriteBatch spriteBatch, Vector2 position, Color color)
    {
        spriteBatch.Draw(CreatePixelTexture(spriteBatch.GraphicsDevice), position, color);
    }

    /// <summary>
    /// Draws a line between two points with the specified color.
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used for drawing.</param>
    /// <param name="start">The starting point of the line.</param>
    /// <param name="end">The ending point of the line.</param>
    /// <param name="color">The color of the line.</param>
    public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
    {
        Vector2 edge = end - start;
        float angle = (float)Math.Atan2(edge.Y, edge.X);

        spriteBatch.Draw(CreatePixelTexture(spriteBatch.GraphicsDevice), start, null, color, angle, Vector2.Zero, new Vector2(edge.Length(), 1), SpriteEffects.None, 0);
    }

    /// <summary>
    /// Draws a square at the specified position with the given size and color.
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used for drawing.</param>
    /// <param name="position">The position of the square.</param>
    /// <param name="size">The size of the square.</param>
    /// <param name="color">The color of the square.</param>
    public static void DrawSquare(SpriteBatch spriteBatch, Vector2 position, Vector2 size, Color color)
    {
        DrawLine(spriteBatch, position, new(position.X + size.X, position.Y), color);
        DrawLine(spriteBatch, new(position.X + size.X, position.Y), position + size, color);
        DrawLine(spriteBatch, position + size, new(position.X, position.Y + size.Y), color);
        DrawLine(spriteBatch, new(position.X, position.Y + size.Y), position, color);
    }

    /// <summary>
    /// Draws a circle at the specified position with the given radius and color.
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used for drawing.</param>
    /// <param name="position">The position of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="color">The color of the circle.</param>
    /// <param name="segments">The number of segments to use for drawing the circle.</param>
    public static void DrawCircle(SpriteBatch spriteBatch, Vector2 position, float radius, Color color, int segments = 16)
    {
        float angle = MathHelper.TwoPi / segments;
        float currentAngle = 0;

        for (int i = 0; i < segments; i++)
        {
            Vector2 start = new((float)Math.Cos(currentAngle) * radius, (float)Math.Sin(currentAngle) * radius);
            Vector2 end = new((float)Math.Cos(currentAngle + angle) * radius, (float)Math.Sin(currentAngle + angle) * radius);

            DrawLine(spriteBatch, position + start, position + end, color);

            currentAngle += angle;
        }
    }
}