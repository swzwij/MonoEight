using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEight;

public class Player : GameObject
{
    Texture2D _texture;

    public Player(Vector2 position, int layer) : base(position, layer)
    {
        Transform.Position = new Vector2(64, 64);
        _texture = ContentLoader.Load<Texture2D>("PlayerTest");
    }

    public override void Update(GameTime gameTime)
    {
        Vector2 input = Input.InputAxis;
        Transform.Position += input * 2;

        if (Input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
            Transform.Rotation += 0.1f;
        if (Input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Q))
            Transform.Rotation -= 0.1f;

        Camera.Position = Transform.Position;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, Transform.Position, null, Color.White, Transform.Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), Transform.Scale, SpriteEffects.None, 0);
    }
}