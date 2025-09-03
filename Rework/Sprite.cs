using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class Sprite
{
    private readonly SpriteBatch _spriteBatch;
    private readonly Texture2D _texture;
    private readonly Point _position;

    public Rectangle Rectangle { get; set; }
    public Color Color { get; set; } = Color.White;
    public float Rotation { get; set; } = 0;
    public float Scale { get; set; } = 1;
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public float Layer { get; set; } = 0;

    public Sprite(SpriteBatch spriteBatch, Texture2D texture, Point position)
    {
        _spriteBatch = spriteBatch;
        _texture = texture;
        _position = position;
    }

    public void Draw(bool isCentered = true)
    {
        Vector2 origin = isCentered
            ? new(_texture.Width / 2, _texture.Height / 2)
            : Vector2.Zero;
        Vector2 position = _position.ToVector2();

        _spriteBatch.Draw
        (
            _texture,
            position,
            Rectangle,
            Color,
            Rotation,
            origin,
            Scale,
            SpriteEffects,
            Layer
        );
    }
}