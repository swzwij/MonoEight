using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class LoadingState : State
{
    private const float DISPLAY_TIME = 2f;
    private const string ENGINE_NAME = "MonoEight";

    private float _timer = 0;

    private Canvas _canvas;
    private Animator _animator;

    public override void Initialize()
    {
        _canvas = new();

        Camera.BackgroundColor = Color.Black;

        SpriteSheet spriteSheet = new(ContentLoader.Load<Texture2D>("Engine/MonoEight"), new Point(64, 64));
        _animator = new(spriteSheet, DISPLAY_TIME / spriteSheet.SpriteCount, true);
        _animator.Play();

        Camera.RelativePosition = Vector2.Zero;
    }

    public override void Update(GameTime gameTime)
    {
        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer >= DISPLAY_TIME)
            StateManager.ChangeState("Title");

        _animator.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _canvas.DrawText
        (
            spriteBatch,
            ENGINE_NAME,
            FontSize.H2,
            new Vector2(GameWindow.Width / 2, 16),
            Color.White,
            Alignment.TopCenter
        );
        _animator.Draw(spriteBatch, Vector2.Zero);
    }
}