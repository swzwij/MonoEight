using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEight;

public class Player : GameObject
{
    // private NewAnimator _animator;
    public Player(string texture)
    {
        // SpriteRenderer _ = new(this, Content.Load<Texture2D>(texture));
        // _animator = new(this, new(Content.Load<Texture2D>(texture), 16),
        // [new("Idle", 0, 1), new("Squish", 0, 2, 3, 2, 0)]);
    }

    private void Awake()
    {
        // _animator.Play("Idle");
    }

    private void Update()
    {
        float deltaTime = Time.DeltaTime * 10;
        Position += new Vector2(Input.InputAxis.X, Input.InputAxis.Y) * deltaTime;

        // if (Input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.E))
            // _animator.Play("Squish");
    }
}