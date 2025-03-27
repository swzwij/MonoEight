using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class Canvas
{
    private readonly Dictionary<FontSize, SpriteFont> _fonts;

    public Canvas()
    {
        _fonts = new()
        {
            { FontSize.H1, ContentLoader.Load<SpriteFont>("Engine/Font/h1") },
            { FontSize.H2, ContentLoader.Load<SpriteFont>("Engine/Font/h2") },
            { FontSize.P, ContentLoader.Load<SpriteFont>("Engine/Font/p") }
        };
    }

    public void DrawText(SpriteBatch spriteBatch, string text, FontSize fontSize, Vector2 position, Color color, Alignment alignment = Alignment.MiddleCenter)
    {
        SpriteFont font = _fonts[fontSize];

        Vector2 textSize = font.MeasureString(text);
        Vector2 alignmentOffset = CalculateAlignment(textSize, position, alignment);

        position += alignmentOffset;
        position += Camera.Position;

        spriteBatch.DrawString(font, text, position, color, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
    }

    public void DrawSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Color color, Alignment alignment = Alignment.MiddleCenter, int scale = 1)
    {
        Vector2 alignmentOffset = CalculateAlignment(texture.Bounds.Size.ToVector2(), position, alignment, scale);

        position += alignmentOffset;
        position += Camera.Position;

        spriteBatch.Draw(texture, position, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
    }

    private Vector2 CalculateAlignment(Vector2 textSize, Vector2 position, Alignment alignment, int scale = 1)
    {
        textSize *= scale;
        return alignment switch
        {
            Alignment.TopLeft => new Vector2(0, 0),
            Alignment.TopCenter => new Vector2(-textSize.X / 2, 0),
            Alignment.TopRight => new Vector2(-textSize.X, 0),
            Alignment.MiddleLeft => new Vector2(0, -textSize.Y / 2),
            Alignment.MiddleCenter => new Vector2(-textSize.X / 2, -textSize.Y / 2),
            Alignment.MiddleRight => new Vector2(-textSize.X, -textSize.Y / 2),
            Alignment.BottomLeft => new Vector2(0, -textSize.Y),
            Alignment.BottomCenter => new Vector2(-textSize.X / 2, -textSize.Y),
            Alignment.BottomRight => new Vector2(-textSize.X, -textSize.Y),
            _ => Vector2.Zero,
        };
    }
}