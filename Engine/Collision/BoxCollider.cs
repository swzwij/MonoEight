using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class BoxCollider
{
    private Vector2 _position;
    private Vector2 _size;
    private Vector2 _offset;

    public Vector2 Position => _position;
    public Vector2 Size => _size;

    public BoxCollider(Vector2 size, Vector2 offset)
    {
        _size = size;
        _offset = offset;
    }

    public void Update(Vector2 position)
    {
        _position = position + _offset;
    }

    public bool Intersects(BoxCollider other)
    {
        Vector2 position = _position + _offset;
        Vector2 otherPosition = other.Position + other._offset;

        return position.X < otherPosition.X + other.Size.X &&
               position.X + Size.X > otherPosition.X &&
               position.Y < otherPosition.Y + other.Size.Y &&
               position.Y + Size.Y > otherPosition.Y;
    }

    public bool Intersects(CircleCollider other)
    {
        return other.Intersects(this);
    }

    public void Draw(SpriteBatch spriteBatch, Color color)
    {
        Debugger.DrawSquare(spriteBatch, _position, _size, color);
    }
}