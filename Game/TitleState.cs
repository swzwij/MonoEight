using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class TitleState : GameState
{
    private Texture2D _title;

    public override void Initialize()
    {
        base.Initialize();
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
        Vector2 center = new(GameWindow.Width / 2 - _title.Width / 2, GameWindow.Height / 2 - _title.Height / 2);
        spriteBatch.Draw(_title, center, Color.White);

        base.Draw(spriteBatch);
    }
}