using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class TitleState : GameState
{
    private Texture2D _title;
    private SpriteFont _font;

    public override void Initialize()
    {
        base.Initialize();
        Camera.Position = Vector2.Zero;
    }

    public override void LoadContent()
    {
        _title = ContentLoader.Load<Texture2D>("Engine/MonoEight");
        _font = ContentLoader.Load<SpriteFont>("Engine/Font/p");
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.IsStartPressed || Input.IsActionKeyPressed)
            GameStateManager.Instance.ChangeState("Game");

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Vector2 center = new(GameWindow.Width / 2 - _title.Width / 2, GameWindow.Height / 2 - _title.Height / 2);
        spriteBatch.Draw(_title, Vector2.Zero, Color.White);

        string text = "Press Start to Begin";
        Vector2 textSize = _font.MeasureString(text);
        Vector2 textPosition = new(GameWindow.Width / 2 - textSize.X / 2, GameWindow.Height - textSize.Y * 2);
        textPosition += Camera.Position;
        spriteBatch.DrawString(_font, text, textPosition, Color.White);

        base.Draw(spriteBatch);
    }
}