
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class TestScene2 : Scene
{
    private const float _time = 0.05f;
    private float _timer;
    private int _index;
    private SpriteSheet _sheet;

    public override void Awake()
    {
        Camera.BackgroundColor = MEColors.Black;
        _index = 0;
        _timer = 0;
        base.Awake();
    }

    public override void LoadContent()
    {
        _sheet = new(Content.LoadFromRoot<Texture2D>("Assets", "MonoEightAnimation"), 64);
        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.IsKeyPressed(Keys.Space))
            SceneManager.Load("Test 1");

        if (_index == _sheet.Count - 1)
            return;

        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer > _time)
        {
            _index++;
            _index %= _sheet.Count;
            _timer = 0;
        }

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _sheet.Draw(spriteBatch, _index, Point.Zero);

        base.Draw(spriteBatch);
    }
}