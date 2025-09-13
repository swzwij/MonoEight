
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class TestScene2 : Scene
{
    private SpriteRenderer _renderer;

    public override void Awake()
    {
        Camera.BackgroundColor = MEColors.Black;
        base.Awake();
    }

    public override void LoadContent()
    {
        _renderer = new(Content.LoadFromRoot<Texture2D>("Assets", "MonoEightText"));
        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.IsKeyPressed(Keys.Space))
            SceneManager.Load("Test 1");

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _renderer.Draw(spriteBatch, Point.Zero);
        base.Draw(spriteBatch);
    }
}