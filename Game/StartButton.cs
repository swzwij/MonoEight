using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEight;
using GameWindow = MonoEight.GameWindow;

public class StartButton : GameObject
{
    private readonly ClickableCollider _collider;
    private readonly Canvas _canvas;

    public StartButton(Vector2 position, Vector2 Size) : base(position, 0, 0)
    {
        _collider = new ClickableCollider(position, Size);
        _collider.OnClick += StartGame;

        _canvas = new Canvas();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _collider.Draw(spriteBatch, Color.White);
        string text = "Start";
        _canvas.DrawText(spriteBatch, text, FontSize.P, new(GameWindow.Size.X/2, GameWindow.Size.Y/2), Color.White, Alignment.MiddleCenter);
    }

    public override void Update(GameTime gameTime)
    {
        _collider.Update(Transform.Position);
    }

    private void StartGame()
    {
        StateManager.ChangeState("Game");
    }
}