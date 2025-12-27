using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEight;
using System;

public class NewPlayer2 : GameObject
{
    private CircleCollider _collider;
    private SpriteRenderer _renderer;
    private Vector2 _velocity;
    private Vector2 _clickPosition;
    private bool _hasClicked;
    private float _speed = 200f;

    public NewPlayer2(string texture, int x)
    {
        Position = new Vector2(x, 0);

        SpriteSheet sheet = new(Content.Load<Texture2D>(texture), 16);
        _renderer = new SpriteRenderer(sheet[0]);
        _collider = new CircleCollider(16);
    }

    protected override void Initialize()
    {
        //AddComponent(_renderer);
        AddComponent(_collider);
    }

    protected override void Update()
    {
        if (Input.Mouse.LeftPressed)
        {
            if (!_collider.Intersects(Input.Mouse.Position.Int()))
                return;

            _clickPosition = Input.Mouse.Position;
            _hasClicked = true;
        }

        if (Input.Mouse.LeftReleased)
        {
            if (!_hasClicked)
                return;

            Vector2 direction = _clickPosition - Input.Mouse.Position;
            direction.Normalize();
            float speed = Vector2.Distance(_clickPosition, Input.Mouse.Position) * 6f;
            _velocity = direction * speed;
            _hasClicked = false;
        }

        _velocity *= 0.9f;

        if (_velocity.LengthSquared() < .1f)
            _velocity = Vector2.Zero;

        Position += _velocity * Time.DeltaTime;

        if (_velocity.LengthSquared() > .1f)
        {
            foreach (Collider collider in Scene.Colliders)
            {
                if (collider == _collider)
                    continue;

                if (_collider.Intersects(collider))
                {
                    Position -= _velocity * Time.DeltaTime;
                    _velocity = -_velocity * 0.75f;
                    break;
                }
            }
        }
    }
}
