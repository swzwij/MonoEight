using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class LoadingScene : Scene
{
    private const float WAIT_TIME = 1f;

    private float _time;
    private float _timer;

    private Animation _animation;

    public override void Awake()
    {
        _timer = 0;
    }

    public override void LoadContent()
    {
        Camera.BackgroundColor = MEColors.Black;

        _animation = new(new(Content.LoadFromRoot<Texture2D>("Assets", "MonoEightAnimation"), 64))
        {
            FrameDuration = 0.05f,
            Loop = false,
            Scale = MEWindow.Width / 64 / 2
        };

        // _animator.Scale = MEWindow.Width / _logo.Width / 2;

        _time = _animation.Duration + WAIT_TIME;

        _animation.Play();

        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _timer += deltaTime;

        if (_timer >= _time)
            SceneManager.Load("Test 1");

        _animation.Update(gameTime);

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _animation.Draw(spriteBatch, new(0, -3));
        base.Draw(spriteBatch);
    }
}