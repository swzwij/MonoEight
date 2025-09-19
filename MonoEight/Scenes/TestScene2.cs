using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class TestScene2 : Scene
{
    private SquareCollider _squareB;
    private SquareCollider _collider;

    private float _speed = 5;

    public override void Awake()
    {
        Camera.BackgroundColor = MEColors.Black;

        _squareB = new(this, new(32, 0), 12);

        _collider = new(this, new(0, 0), 8);
        _collider.OnCollisionEnter += () => { Console.WriteLine("CollisionEnter"); };
        _collider.OnCollisionExit += () => { Console.WriteLine("CollisionExit"); };
        _collider.OnCollisionStay += () => { Console.WriteLine("CollisionStay"); };

        Input.Action("A").OnPressed += () => Console.WriteLine("Pressed");

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
        _squareB.Position += new Vector2(Input.InputAxis.X, Input.InputAxis.Y) * deltaTime;

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _collider.Draw(spriteBatch);
        _squareB.Draw(spriteBatch);
        base.Draw(spriteBatch);
    }
}