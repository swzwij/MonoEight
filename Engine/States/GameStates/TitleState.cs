using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class TitleState : GameState
{
    private Texture2D _title;
    private Canvas _canvas;

    public override void Initialize()
    {
        base.Initialize();
        Camera.BackgroundColor = Color.Black;
        Camera.RelativePosition = Vector2.Zero;
        _canvas = new();
    }

    public override void LoadContent()
    {
        _title = ContentLoader.Load<Texture2D>("Engine/MonoEight");
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.IsStartPressed || Input.IsActionKeyPressed)
            GameStateManager.Instance.ChangeState("Game");

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Sprite.DrawCentered(spriteBatch, _title);

        string text = "Press Start to Begin";
        _canvas.DrawText(spriteBatch, text, FontSize.P, new(GameWindow.Width / 2, GameWindow.Height), Color.White, Alignment.BottomCenter);

        // Debugger.DrawPixel(spriteBatch, Vector2.Zero, Color.Magenta);

        base.Draw(spriteBatch);
    }
}