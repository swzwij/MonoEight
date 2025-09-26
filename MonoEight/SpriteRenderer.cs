using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class SpriteRenderer : Component
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

    public Point Offset { get; set; } = Point.Zero;
    public Color Color { get; set; } = Color.White;
    public float Rotation { get; set; } = 0;
    public Vector2 Origin { get; set; } = Vector2.Zero;
    public float Scale { get; set; } = 1;
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public float Layer { get; set; } = 0;

    private bool _hasTexture;

    public SpriteRenderer(GameObject gameObject, Texture2D texture) : base(gameObject)
    {
        Texture = texture;
    }

    public SpriteRenderer(GameObject gameObject) : base(gameObject)
    {

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!_hasTexture)
            return;

        Console.WriteLine("Draw SpriteRenderer");

        spriteBatch.Draw
        (
            _texture,
            (GameObject.Position.Int() + Offset).Float(),
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