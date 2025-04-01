using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public static class Sprite
{
    private static GraphicsDevice _graphicsDevice;

    public static void Initialize(GraphicsDevice graphicsDevice)
    {
        _graphicsDevice = graphicsDevice;
    }

    public static void DrawCentered(SpriteBatch spriteBatch, Texture2D texture, Vector2 position)
    {
        Vector2 origin = new(texture.Width / 2, texture.Height / 2);
        spriteBatch.Draw(texture, position, null, Color.White, 0, origin, 1, SpriteEffects.None, 0);
    }

    public static void DrawCentered(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, float rotation)
    {
        Vector2 origin = new(texture.Width / 2, texture.Height / 2);
        spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1, SpriteEffects.None, 0);
    }

    public static void DrawCentered(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, float rotation, float scale)
    {
        Vector2 origin = new(texture.Width / 2, texture.Height / 2);
        spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
    }

    public static void DrawCentered(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Color color)
    {
        Vector2 origin = new(texture.Width / 2, texture.Height / 2);
        spriteBatch.Draw(texture, position, null, color, 0, origin, 1, SpriteEffects.None, 0);
    }

    public static void DrawCentered(SpriteBatch spriteBatch, Texture2D texture)
    {
        DrawCentered(spriteBatch, texture, Vector2.Zero);
    }

    public static Texture2D Square(int size, Color color)
    {
        Texture2D texture = new(_graphicsDevice, size, size);
        Color[] data = new Color[size * size];

        for (int i = 0; i < data.Length; i++)
            data[i] = color;

        texture.SetData(data);
        return texture;
    }

    public static Texture2D Circle(int radius, Color color)
    {
        Texture2D texture = new(_graphicsDevice, radius * 2, radius * 2);
        Color[] data = new Color[radius * 2 * radius * 2];

        for (int y = 0; y < radius * 2; y++)
        {
            for (int x = 0; x < radius * 2; x++)
            {
                float dx = x - radius + 0.5f;
                float dy = y - radius + 0.5f;
                if (dx * dx + dy * dy <= radius * radius)
                    data[y * radius * 2 + x] = color;
                else
                    data[y * radius * 2 + x] = Color.Transparent;
            }
        }

        texture.SetData(data);
        return texture;
    }
}