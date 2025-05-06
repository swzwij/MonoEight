using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class TitleState : State
{
    private Texture2D _title;
    private Canvas _canvas;

    public override void Initialize()
    {
        base.Initialize();
        Camera.BackgroundColor = Color.Black;
        Camera.RelativePosition = Vector2.Zero;
        _canvas = new();

        StartButton startButton = new(Vector2.Zero, new Vector2(200, 50));
        Add(startButton);
    }
    public override void LoadContent()
    {
        _title = ContentLoader.LoadFromRoot<Texture2D>("Assets", "MonoEight");
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.IsStartPressed || Input.IsActionKeyPressed)
            StateManager.ChangeState("Game");
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        string text = "Press Start to Begin";
        _canvas.DrawText(spriteBatch, text, FontSize.P, new(GameWindow.Width / 2, GameWindow.Height), Color.White, Alignment.BottomCenter);
    }
}