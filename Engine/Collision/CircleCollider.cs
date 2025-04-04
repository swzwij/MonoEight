using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

/// <summary>
/// Represents a circle collider for 2D collision detection.
/// </summary>
public class CircleCollider
{
    private Vector2 _position;
    private float _radius;

    public Vector2 Position => _position;
    public float Radius => _radius;

    public CircleCollider(Vector2 position, float radius)
    {
        _position = position;
        _radius = radius;
    }

    public void Update(Vector2 position)
    {
        _position = position;
    }

    /// <summary>
    /// Checks if this circle collider intersects with another circle collider.
    /// </summary>
    /// <param name="other">The other circle collider to check for intersection.</param>
    public bool Intersects(CircleCollider other)
    {
        float sqrDistance = Vector2.DistanceSquared(_position, other.Position);
        float radiusSum = _radius + other.Radius;
        return sqrDistance < radiusSum * radiusSum;
    }

    /// <summary>
    /// Checks if this circle collider intersects with a box collider.
    /// </summary>
    /// <param name="other">The box collider to check for intersection.</param>
    /// <returns>True if the colliders intersect, false otherwise.</returns>
    public bool Intersects(BoxCollider other)
    {
        Vector2 position = _position;
        Vector2 otherPosition = other.Position;

        float closestX = MathHelper.Clamp(position.X, otherPosition.X, otherPosition.X + other.Size.X);
        float closestY = MathHelper.Clamp(position.Y, otherPosition.Y, otherPosition.Y + other.Size.Y);

        float distanceX = position.X - closestX;
        float distanceY = position.Y - closestY;

        float distanceSquared = distanceX * distanceX + distanceY * distanceY;
        return distanceSquared < _radius * _radius;
    }

    public void Draw(SpriteBatch spriteBatch, Color color, int segments = 16)
    {
        Debugger.DrawCircle(spriteBatch, _position, _radius, color, segments);
    }
}