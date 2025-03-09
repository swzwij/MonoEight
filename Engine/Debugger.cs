using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public static class Debugger
{
    public static Texture2D CreatePixelTexture(GraphicsDevice graphicsDevice)
    {
        Texture2D Pixel = new(graphicsDevice, 1, 1);
        Pixel.SetData([Color.White]);
        return Pixel;
    }

    public static void DrawPixel(SpriteBatch spriteBatch, Vector2 position, Color color)
    {
        spriteBatch.Draw(CreatePixelTexture(spriteBatch.GraphicsDevice), position, color);
    }

    public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
    {
        Vector2 edge = end - start;
        float angle = (float)Math.Atan2(edge.Y, edge.X);

        spriteBatch.Draw(CreatePixelTexture(spriteBatch.GraphicsDevice), start, null, color, angle, Vector2.Zero, new Vector2(edge.Length(), 1), SpriteEffects.None, 0);
    }

    public static void DrawRectangle(SpriteBatch spriteBatch, Vector2 position, Vector2 size, Color color)
    {
        DrawLine(spriteBatch, position, new(position.X + size.X, position.Y), color);
        DrawLine(spriteBatch, new(position.X + size.X, position.Y), position + size, color);
        DrawLine(spriteBatch, position + size, new(position.X, position.Y + size.Y), color);
        DrawLine(spriteBatch, new(position.X, position.Y + size.Y), position, color);
    }
}