using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class LoadingState : GameState
{
    private const float DISPLAY_TIME = 1f;
    private const string ENGINE_NAME = "MonoEight";

    private SpriteFont _font;
    private float _timer = 0;

    private Canvas _canvas;
    private Animator _animator;

    public override void Initialize()
    {
        _font = ContentLoader.Load<SpriteFont>("Engine/Font/h1");
        _canvas = new();

        SpriteSheet spriteSheet = new(ContentLoader.Load<Texture2D>("Engine/MonoEight"), new Point(144, 128));
        float frameDuration = DISPLAY_TIME / spriteSheet.SpriteCount;
        _animator = new(spriteSheet, frameDuration, true);
        _animator.Play();
    }

    public override void Update(GameTime gameTime)
    {
        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer >= DISPLAY_TIME)
            GameStateManager.Instance.ChangeState("Title");

        _animator.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _canvas.DrawText(spriteBatch, ENGINE_NAME, FontSize.H1, new Vector2(GameWindow.Width / 2, GameWindow.Height / 2), Color.White, Alignment.MiddleCenter);
        _animator.Draw(spriteBatch, Vector2.Zero);
    }
}