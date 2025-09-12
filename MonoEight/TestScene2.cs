
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class TestScene2 : Scene
{
    private Animation _animation;

    public override void Awake()
    {
        Camera.BackgroundColor = MEColors.Black;
        base.Awake();
    }

    public override void LoadContent()
    {
        _animation = new(new(Content.LoadFromRoot<Texture2D>("Assets", "MonoEightAnimation"), 64))
        {
            FrameDuration = 0.05f
        };
        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.IsKeyPressed(Keys.Space))
            SceneManager.Load("Test 1");

        _animation.Update(gameTime);

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _animation.Draw(spriteBatch, Point.Zero);
        base.Draw(spriteBatch);
    }
}