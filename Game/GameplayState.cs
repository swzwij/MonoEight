using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class GameplayState : GameState
{
    private Texture2D _background;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void LoadContent()
    {
        _background = ContentLoader.Load<Texture2D>("Debug");
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.IsBackPressed)
            GameStateManager.Instance.ChangeState("Title");

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_background, Vector2.Zero, Color.White);

        base.Draw(spriteBatch);
    }
}