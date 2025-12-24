using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

/// <summary>
/// Provides methods for drawing text onto a scene using different font sizes and styles.
/// </summary>
/// <remarks>
/// The Canvas class manages font resources and coordinates text rendering relative to the scene's
/// camera. It is intended to be used as a utility for drawing text elements within a graphical scene. Instances of
/// Canvas are typically associated with a specific Scene and should be reused for consistent font management.
/// </remarks>
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