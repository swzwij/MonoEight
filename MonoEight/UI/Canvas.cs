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
            { FontSize.S, Content.LoadFromRoot<SpriteFont>("Assets", "Fonts/MonoEight") },
            { FontSize.M, Content.LoadFromRoot<SpriteFont>("Assets", "Fonts/MonoEightMedium") },
            // { FontSize.M, Content.LoadFromRoot<SpriteFont>("Assets", "Fonts/M") },
            // { FontSize.L, Content.LoadFromRoot<SpriteFont>("Assets", "Fonts/L") },
            // { FontSize.Test, Content.LoadFromRoot<SpriteFont>("Assets", "Fonts/Blocky") },
        };
    }
    
    public void DrawText(SpriteBatch spriteBatch, string text, FontSize size, Point position, Color color)
    {
        // TODO
        SpriteFont font = _fonts[size];
        Vector2 textSize = font.MeasureString(text);
        Vector2 offset = new((int)(-textSize.X / 2), (int)(-textSize.Y / 2));
        Point newPosition = position + offset.ToPoint() + _scene.Camera.Position.ToPoint();
        spriteBatch.DrawString(font, text, newPosition.ToVector2(), color, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
    }
}