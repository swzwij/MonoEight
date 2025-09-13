
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class TestScene2 : Scene
{
    private Animator _animator;

    public override void Awake()
    {
        Camera.BackgroundColor = MEColors.Black;
        base.Awake();
    }

    public override void LoadContent()
    {
        _animator = new Animator
        (
            [
                new("default", new(new(Content.LoadFromRoot<Texture2D>("Assets", "MonoEightAnimation"), 64))
                {
                    FrameDuration = 0.05f,
                    Loop = false
                })
            ]
        );

        _animator.Play("default");

        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.IsKeyPressed(Keys.Space))
            SceneManager.Load("Test 1");

        _animator.Update(gameTime);

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _animator.Draw(spriteBatch, Point.Zero);
        base.Draw(spriteBatch);
    }
}