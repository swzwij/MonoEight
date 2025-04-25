using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class GameState : State
{
    private Texture2D _background;
    private Player _player;
    private Canvas _canvas;

    public override void Initialize()
    {
        base.Initialize();

        Camera.BackgroundColor = Color.DarkMagenta;

        _player = new Player(Vector2.Zero, 0);
        Add(_player);

        _canvas = new();
    }

    public override void LoadContent()
    {
        _background = ContentLoader.Load<Texture2D>("Debug");
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.IsBackPressed)
            StateManager.ChangeState("Title");

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Sprite.DrawCentered(spriteBatch, _background);

        string text = "Gameplay State";
        _canvas.DrawText(spriteBatch, text, FontSize.P, new(GameWindow.Width / 2, GameWindow.Height), Color.White, Alignment.BottomCenter);

        Debugger.DrawPixel(spriteBatch, Vector2.Zero, Color.Magenta);

        base.Draw(spriteBatch);
    }
}