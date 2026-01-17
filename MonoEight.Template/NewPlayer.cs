using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEight.Core;
using MonoEight.Core.UserInput;
using MonoEight.Core.Physics;
using MonoEight.Core.Sprite;

namespace MonoEight;

public class NewPlayer : GameObject
{
    private BoxCollider _collider;
    private SpriteRenderer _renderer;
    private Vector2 _velocity;
    private Vector2 _clickPosition;
    private bool _hasClicked;
    private float _speed = 200f;

    public NewPlayer(string texture, int x)
    {
        Position = new Vector2(x, 0);

        SpriteSheet sheet = new(Content.Load<Texture2D>(texture), 16);
        _renderer = new SpriteRenderer(sheet[0]);
        _collider = new BoxCollider(16);
    }

    protected override void Initialize()
    {
        //AddComponent(_renderer);
        AddComponent(_collider);
    }

    protected override void Update()
    {
        Vector2 position = Position;
        position.X += Input.IsPressed("A") ? 1f : 0f;
        position.Y += Input.IsPressed("B") ? 1f : 0f;
        Position = position;    
        
        GameTime t = Time.GameTime;
        
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
                    Position -= _velocity * Time.DeltaTime / 2;
                    _velocity = -_velocity * 0.75f;
                    break;
                }
            }
        }
    }

    //protected override void Draw(SpriteBatch spriteBatch)
    //{
    //    _collider.Draw(spriteBatch);
    //}
}
