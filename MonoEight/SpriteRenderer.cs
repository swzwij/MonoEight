using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class NewSpriteRenderer : IComponent
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

    public Point Offset { get; set; } = Point.Zero;
    public Color Color { get; set; } = Color.White;
    public float Rotation { get; set; } = 0;
    public Vector2 Origin { get; set; } = Vector2.Zero;
    public float Scale { get; set; } = 1;
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
    public float Layer { get; set; } = 0;

    public bool IsActive { get; set; } = true;
    public GameObject GameObject { get; set; }

    public NewSpriteRenderer(GameObject gameObject, Texture2D texture)
    {
        Texture = texture;
        GameObject = gameObject;
        GameObject.Add(this);
    }

    public void Awake()
    {

    }

    public void Update(GameTime gameTime)
    {

    }

    public void Draw(SpriteBatch spriteBatch)
    {
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
}