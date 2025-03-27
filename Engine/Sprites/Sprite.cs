using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public static class Sprite
{
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
}