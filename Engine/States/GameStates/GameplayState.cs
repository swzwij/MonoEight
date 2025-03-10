using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class GameplayState : GameState
{
    private Texture2D _background;
    private Player _player;
    private SpriteFont _font;

    public override void Initialize()
    {
        base.Initialize();

        _player = new Player(Vector2.Zero, 0);
        Add(_player);
    }

    public override void LoadContent()
    {
        _background = ContentLoader.Load<Texture2D>("Debug");
        _font = ContentLoader.Load<SpriteFont>("Engine/Font/p");
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.IsBackPressed)
            GameStateManager.Instance.ChangeState("Title");

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Sprite.DrawCentered(spriteBatch, _background);

        string text = "Gameplay State";
        Vector2 textSize = _font.MeasureString(text);
        Vector2 textPosition = new(GameWindow.Width / 2 - textSize.X / 2, GameWindow.Height - textSize.Y * 2);
        textPosition += Camera.Position;
        spriteBatch.DrawString(_font, text, textPosition, Color.White);

        Debugger.DrawPixel(spriteBatch, Vector2.Zero, Color.Magenta);

        base.Draw(spriteBatch);
    }
}