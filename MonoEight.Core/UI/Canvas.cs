using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEight.Core.Scenes;

namespace MonoEight.Core.UI;

/// <summary>
/// Manages the rendering of text to the screen for a given <see cref="Scene"/>.
/// </summary>
public class Canvas
{
    private readonly Scene _scene;
    private readonly Dictionary<FontSize, SpriteFont> _fonts;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Canvas"/> class and loads the default fonts.
    /// </summary>
    /// <param name="scene">The <see cref="Scene"/> this canvas belongs to.</param>
    public Canvas(Scene scene)
    {
        _scene = scene;
        _fonts = new Dictionary<FontSize, SpriteFont>
        {
            { FontSize.S, Content.LoadFromRoot<SpriteFont>("Assets", "Fonts/MonoEight") },
            { FontSize.M, Content.LoadFromRoot<SpriteFont>("Assets", "Fonts/MonoEightMedium") },
        };
    }
    
    /// <summary>
    /// Draws a string of text centered at the given position.
    /// </summary>
    /// <param name="spriteBatch"><see cref="SpriteBatch"/></param>
    /// <param name="text">The string of text to display.</param>
    /// <param name="size">The font to use.</param>
    /// <param name="position">The screen space point where the text will be drawn.</param>
    /// <param name="color">The color of the text.</param>
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
