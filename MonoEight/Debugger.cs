using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public static class Debugger
{
    private static Texture2D _pixel;

    public static void Initialize(GraphicsDeviceManager graphics)
    {
        _pixel = new(graphics.GraphicsDevice, 1, 1);
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
}