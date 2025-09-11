using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class LoadingScene : Scene
{
    private const float LOAD_TIME = 2f;

    private Texture2D _logo;
    private SpriteRenderer _spriteRenderer;
    private float _timer;

    public override void LoadContent()
    {
        Camera.BackgroundColor = MEColors.Black;
        _logo = Content.LoadFromRoot<Texture2D>("Assets", "MonoEightLogo");
        _spriteRenderer = new(_logo);
        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _timer += deltaTime;

        if (_timer >= LOAD_TIME)
            SceneManager.Load("Test 1");

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Console.WriteLine(Camera.Position);
        _spriteRenderer.Draw(spriteBatch, Point.Zero);
        base.Draw(spriteBatch);
    }
}