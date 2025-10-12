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
            _hasTexture = true;
        }
    }

    public Vector2 Position { get; set; } = Vector2.Zero;
    public Point Offset { get; set; } = Point.Zero;
    public Color Color { get; set; } = Color.White;
    public float Rotation { get; set; } = 0;
    public Vector2 Origin { get; set; } = Vector2.Zero;
    public float Scale { get; set; } = 1;
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public float Layer { get; set; } = 0;

    private bool _hasTexture;

    public SpriteRenderer(Texture2D texture)
    {
        Texture = texture;
    }

    public void Draw(SpriteBatch spriteBatch)
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