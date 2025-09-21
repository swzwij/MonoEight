using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEight;

public class Player : GameObject
{
    public Player(string texture)
    {
        SpriteRenderer _ = new(this, Content.Load<Texture2D>(texture));
    }

    private void Awake()
    {
        Console.WriteLine("Awoken Player");
    }

    private void Update()
    {
        float deltaTime = Time.DeltaTime * 10;
        Position += new Vector2(Input.InputAxis.X, Input.InputAxis.Y) * deltaTime;
    }
}