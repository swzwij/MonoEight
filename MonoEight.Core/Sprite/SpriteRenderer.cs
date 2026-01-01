using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight.Core;

/// <summary>
/// Represents a component that renders a 2D texture (sprite) with configurable position, color, rotation, scale, and
/// rendering effects.
/// </summary>
public class SpriteRenderer : Component
{
    private Texture2D _texture;
    private bool _hasTexture;

    public Texture2D Texture
    {
        get => _texture;
        set
        {
            _texture = value;
            if (_texture != null)
            {
                Origin = new(_texture.Width / 2f, _texture.Height / 2f);
                _hasTexture = true;
            }
        }
    }

    public Point Offset { get; set; } = Point.Zero;
    public Color Color { get; set; } = Color.White;
    public float Rotation { get; set; } = 0;
    public Vector2 Origin { get; set; } = Vector2.Zero;
    public float Scale { get; set; } = 1;
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public float Layer { get; set; } = 0;

    public SpriteRenderer() { }

    public SpriteRenderer(Texture2D texture)
    {
        Texture = texture;
    }

    protected override void Draw(SpriteBatch spriteBatch)
    {
        if (!_hasTexture)
            return;

        spriteBatch.Draw
        (
            _texture,
            (Position.Int() + Offset).Float(),
            null,
            Color,
            Rotation,
            Origin,
            Scale,
            SpriteEffects,
            Layer
        );
    }
}
