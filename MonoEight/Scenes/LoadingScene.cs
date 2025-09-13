using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class LoadingScene : Scene
{
    private const float WAIT_TIME = 1f;

    private float _time;
    private float _timer;
    private Animator _animator;

    public override void LoadContent()
    {
        Camera.BackgroundColor = MEColors.Black;

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

        _time = _animator["default"].Duration + WAIT_TIME;

        _animator.Play("default");

        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _timer += deltaTime;

        if (_timer >= _time)
            SceneManager.Load("Test 1");

        _animator.Update(gameTime);

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _animator.Draw(spriteBatch, new(1,0));
        base.Draw(spriteBatch);
    }
}