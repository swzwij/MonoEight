using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class Canvas
{
    private readonly Scene _scene;
    private readonly Dictionary<FontSize, SpriteFont> _fonts;

    public Canvas(Scene scene)
    {
        _scene = scene;

        _fonts = new()
        {
            { FontSize.S, Content.LoadFromRoot<SpriteFont>("Assets", "Fonts/h1") },
            { FontSize.M, Content.LoadFromRoot<SpriteFont>("Assets", "Fonts/h2") },
            { FontSize.L, Content.LoadFromRoot<SpriteFont>("Assets", "Fonts/p") }
        };
    }

    public void DrawText(SpriteBatch spriteBatch, string text, FontSize size, Point position, Color color)
    {
        SpriteFont font = _fonts[size];
        Vector2 textSize = font.MeasureString(text);
        Vector2 offset = new(-textSize.X / 2, -textSize.Y / 2);
        // Vector2 newPosition = position.ToVector2() + offset;
        // Console.WriteLine(_scene.Camera.Position);
        Vector2 newPosition = position.ToVector2() + offset + _scene.Camera.Position.ToVector2();
        spriteBatch.DrawString(font, text, newPosition, color, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
    }
}