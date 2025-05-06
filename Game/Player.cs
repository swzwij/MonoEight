using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEight;

public class Player : GameObject
{
    private Texture2D _texture;
    private BoxCollider _collider;

    public Player(Vector2 position, int layer) : base(position, layer)
    {
        _texture = ContentLoader.Load<Texture2D>("PlayerTest");
        _collider = new BoxCollider(position, new Vector2(32, 32));
    }

    public override void Update(GameTime gameTime)
    {
        Vector2 mousePosition = Input.Mouse.Position;
        Transform.Position = mousePosition;
        _collider.Position = mousePosition;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Sprite.DrawCentered(spriteBatch, _texture, Transform.Position);
        _collider.Draw(spriteBatch, Color.Red);
    }
}