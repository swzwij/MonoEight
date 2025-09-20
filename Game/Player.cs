using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEight;

public class Player : GameObject
{
    private NewSpriteRenderer _renderer;

    public Player(string texture)
    {
        _renderer = new(this, Content.Load<Texture2D>(texture));
    }

    public override void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds * 10;
        Position += new Vector2(Input.InputAxis.X, Input.InputAxis.Y) * deltaTime;

        base.Update(gameTime);
    }
}