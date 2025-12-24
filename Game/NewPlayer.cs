using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEight;
using System;

public class NewPlayer : GameObject
{
    private SpriteRenderer _renderer;
    private Vector2 _velocity;
    private Vector2 _clickPosition;
    private bool _hasClicked;
    private float _speed = 200f;

    public NewPlayer(string texture)
    {
        SpriteSheet sheet = new(Content.Load<Texture2D>(texture), 16);
        _renderer = new SpriteRenderer(sheet[0]);

        AddComponent(_renderer);
    }

    protected override void Update()
    {
        if (Input.Mouse.LeftPressed)
        {
            _clickPosition = Input.Mouse.Position;
            _hasClicked = true;

            //Vector2 direction = _clickPosition - Position;
            //direction.Normalize();
            //_velocity = direction * _speed;
        }

        if (Input.Mouse.LeftReleased)
        {
            if (!_hasClicked)
                return;

            Vector2 direction = _clickPosition - Input.Mouse.Position;
            direction.Normalize();
            float speed = Vector2.Distance(_clickPosition, Input.Mouse.Position) * 6f;
            _velocity = direction * speed;
        }


        _velocity *= 0.9f;
        Position += _velocity * Time.DeltaTime;
    }
}
