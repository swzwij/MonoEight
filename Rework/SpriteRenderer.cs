using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class SpriteRenderer
{
    private Texture2D _texture;
    private Vector2 _origin;

    public Texture2D Texture
    {
        get => _texture;
        set
        {
            _texture = value;
            _origin = new(_texture.Width / 2f, _texture.Height / 2f);
        }
    }

    public Rectangle Rectangle { get; set; }
    public Color Color { get; set; } = Color.White;
    public float Rotation { get; set; } = 0;
    public float Scale { get; set; } = 1;
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public float Layer { get; set; } = 0;


    public SpriteRenderer(Texture2D texture)
    {
        Texture = texture;
    }

    public void Draw(SpriteBatch spriteBatch, Point position)
    {
        spriteBatch.Draw
        (
            _texture,
            position.ToVector2(),
            Rectangle,
            Color,
            Rotation,
            _origin,
            Scale,
            SpriteEffects,
            Layer
        );
    }
}