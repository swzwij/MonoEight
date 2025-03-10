using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEight;

public class Player : GameObject
{
    Texture2D _texture;

    public Player(Vector2 position, int layer) : base(position, layer)
    {
        _texture = ContentLoader.Load<Texture2D>("PlayerTest");
    }

    public override void Update(GameTime gameTime)
    {
        Vector2 input = Input.InputAxis;
        Transform.Position += input * 2;
        Camera.RelativePosition = Transform.Position;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Sprite.DrawCentered(spriteBatch, _texture, Transform.Position);
    }
}