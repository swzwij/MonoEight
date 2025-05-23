using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

/// <summary>
/// Represents a box collider for 2D collision detection.
/// </summary>
public class BoxCollider
{
    private Vector2 _position;
    private Vector2 _size;
    private Vector2 _offset;

    public Vector2 Position
    {
        get => _position;
        set => Update(value);
    }
    public Vector2 Size => _size;
    public Vector2 Offset => _offset;

    public BoxCollider(Vector2 position, Vector2 size, Vector2 offset = default)
    {
        _size = size;
        _offset = offset == default ? new Vector2(-size.X / 2, -size.Y / 2) : offset;
        Update(position);
    }

    public void Update(Vector2 position)
    {
        _position = position + _offset;
    }

    /// <summary>
    /// Checks if this box collider intersects with another box collider.
    /// </summary>
    /// <param name="other">The other box collider to check for intersection.</param>
    /// <returns>True if the colliders intersect, false otherwise.</returns>
    public bool Intersects(BoxCollider other)
    {
        Vector2 position = _position + _offset;
        Vector2 otherPosition = other.Position + other.Offset;

        return position.X < otherPosition.X + other.Size.X &&
               position.X + Size.X > otherPosition.X &&
               position.Y < otherPosition.Y + other.Size.Y &&
               position.Y + Size.Y > otherPosition.Y;
    }

    /// <summary>
    /// Checks if this box collider intersects with a circle collider.
    /// </summary>
    /// <param name="other">The circle collider to check for intersection.</param>
    /// <returns>True if the colliders intersect, false otherwise.</returns>
    public bool Intersects(CircleCollider other)
    {
        return other.Intersects(this);
    }

    public void Draw(SpriteBatch spriteBatch, Color color)
    {
        Debugger.DrawSquare(spriteBatch, _position, _size, color);
    }
}