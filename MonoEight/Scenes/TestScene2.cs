
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class TestScene2 : Scene
{
    private SquareCollider _squareA;
    private SquareCollider _squareB;

    private float _speed = 5;

    public override void Awake()
    {
        Camera.BackgroundColor = MEColors.Black;

        _squareA = new(Point.Zero, new(8));
        _squareB = new(new(32, 0), new(12));

        base.Awake();
    }

    public override void LoadContent()
    {
        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.IsPressed("A"))
            SceneManager.Load("Test 1");

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds * _speed;

        // _position.X += Input.InputAxis.X * deltaTime;
        // _position.Y += Input.InputAxis.Y * deltaTime;

        // _squareB.Position = _position.ToPoint();

        Point pos = Point.Zero;
        pos.X = (int)(Input.InputAxis.X * deltaTime);
        pos.Y = (int)(Input.InputAxis.Y * deltaTime);
        _squareB.Position += pos;

        if (_squareA.Intersects(_squareB))
            Console.WriteLine("Collision");

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _squareA.Draw(spriteBatch);
        _squareB.Draw(spriteBatch);
        base.Draw(spriteBatch);
    }
}