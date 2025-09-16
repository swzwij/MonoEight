using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class SpriteRenderer
{
    private Texture2D _texture;

    public Texture2D Texture
    {
        get => _texture;
        set
        {
            _texture = value;
            Origin = new(_texture.Width / 2f, _texture.Height / 2f);
        }
    }

    public Color Color { get; set; } = Color.White;
    public float Rotation { get; set; } = 0;
    public Vector2 Origin { get; set; } = Vector2.Zero;
    public float Scale { get; set; } = 1;
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public float Layer { get; set; } = 0;

    public SpriteRenderer(Texture2D texture)
    {
        Texture = texture;
    }

    public SpriteRenderer() { }

    public void Draw(SpriteBatch spriteBatch, Point position)
    {
        spriteBatch.Draw
        (
            _texture,
            position.ToVector2(),
            null,
            Color,
            Rotation,
            Origin,
            Scale,
            SpriteEffects,
            Layer
        );
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        Draw(spriteBatch, position.ToPoint());
    }
}