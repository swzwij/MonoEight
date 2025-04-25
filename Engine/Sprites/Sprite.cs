using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

/// <summary>
/// Provides methods for drawing sprites and creating textures in MonoGame.
/// </summary>
public static class Sprite
{
    private static GraphicsDevice _graphicsDevice;
    private static Texture2D _pixelTexture;

    /// <summary>
    /// Initializes the Sprite class with a graphics device.
    /// </summary>
    /// <param name="graphicsDevice">The graphics device to use for creating textures.</param>
    public static void Initialize(GraphicsDevice graphicsDevice)
    {
        _graphicsDevice = graphicsDevice;
        _pixelTexture = Square(1, Color.White);
    }

    /// <summary>
    /// Draws a centered texture at the specified position with the specified color.
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used for drawing.</param>
    /// <param name="texture">The texture to draw.</param>
    /// <param name="position">The position to draw the texture at.</param>
    public static void DrawCentered(SpriteBatch spriteBatch, Texture2D texture, Vector2 position)
    {
        Vector2 origin = new(texture.Width / 2, texture.Height / 2);
        spriteBatch.Draw(texture, position, null, Color.White, 0, origin, 1, SpriteEffects.None, 0);
    }

    /// <summary>
    /// Draws a centered texture at the specified position with the specified rotation.
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used for drawing.</param>
    /// <param name="texture">The texture to draw.</param>
    /// <param name="position">The position to draw the texture at.</param>
    /// <param name="rotation">The rotation of the texture.</param>
    public static void DrawCentered(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, float rotation)
    {
        Vector2 origin = new(texture.Width / 2, texture.Height / 2);
        spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1, SpriteEffects.None, 0);
    }

    /// <summary>
    /// Draws a centered texture at the specified position with the specified rotation and scale.
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used for drawing.</param>
    /// <param name="texture">The texture to draw.</param>
    /// <param name="position">The position to draw the texture at.</param>
    /// <param name="rotation">The rotation of the texture.</param>
    /// <param name="scale">The scale of the texture.</param>
    public static void DrawCentered(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, float rotation, float scale)
    {
        Vector2 origin = new(texture.Width / 2, texture.Height / 2);
        spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
    }

    /// <summary>
    /// Draws a centered texture at the specified position with the specified rotation, scale, and layer.
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used for drawing.</param>
    /// <param name="texture">The texture to draw.</param>
    /// <param name="position">The position to draw the texture at.</param>
    /// <param name="rotation">The rotation of the texture.</param>
    /// <param name="scale">The scale of the texture.</param>
    /// <param name="layer">The layer of the texture.</param>
    public static void DrawCentered(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, float rotation, float scale, float layer)
    {
        Vector2 origin = new(texture.Width / 2, texture.Height / 2);
        spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, layer);
    }

    /// <summary>
    /// Draws a centered texture at the specified position with the specified color.
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used for drawing.</param>
    /// <param name="texture">The texture to draw.</param>
    /// <param name="position">The position to draw the texture at.</param>
    /// <param name="color">The color to tint the texture with.</param>
    public static void DrawCentered(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Color color)
    {
        Vector2 origin = new(texture.Width / 2, texture.Height / 2);
        spriteBatch.Draw(texture, position, null, color, 0, origin, 1, SpriteEffects.None, 0);
    }

    /// <summary>
    /// Draws a centered texture at the origin (Vector2.Zero).
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used for drawing.</param>
    /// <param name="texture">The texture to draw.</param>
    public static void DrawCentered(SpriteBatch spriteBatch, Texture2D texture)
    {
        DrawCentered(spriteBatch, texture, Vector2.Zero);
    }

    /// <summary>
    /// Creates a square texture with the specified size and color.
    /// </summary>
    /// <param name="size">The width and height of the square in pixels.</param>
    /// <param name="color">The color of the square.</param>
    /// <returns>A new Texture2D representing a square.</returns>
    public static Texture2D Square(int size, Color color)
    {
        Texture2D texture = new(_graphicsDevice, size, size);
        Color[] data = new Color[size * size];
        for (int i = 0; i < data.Length; i++)
            data[i] = color;
        texture.SetData(data);
        return texture;
    }

    /// <summary>
    /// Creates a rectangular texture with the specified width, height, and color.
    /// </summary>
    /// <param name="width">The width of the rectangle in pixels.</param>
    /// <param name="height">The height of the rectangle in pixels.</param>
    /// <param name="color">The color of the rectangle.</param>
    /// <returns>A new Texture2D representing a rectangle.</returns>
    public static Texture2D Rectangle(int width, int height, Color color)
    {
        Texture2D texture = new(_graphicsDevice, width, height);
        Color[] data = new Color[width * height];
        for (int i = 0; i < data.Length; i++)
            data[i] = color;
        texture.SetData(data);
        return texture;
    }

    /// <summary>
    /// Creates a circular texture with the specified radius and color.
    /// </summary>
    /// <param name="radius">The radius of the circle in pixels.</param>
    /// <param name="color">The color of the circle.</param>
    /// <returns>A new Texture2D representing a circle with transparent background.</returns>
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

    public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float layer = 0)
    {
        Vector2 direction = end - start;
        float angle = (float)Math.Atan2(direction.Y, direction.X);
        float length = direction.Length();

        spriteBatch.Draw(_pixelTexture, start, null, color, angle, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, layer);
    }
}