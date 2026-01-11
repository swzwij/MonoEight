using Microsoft.Xna.Framework;

namespace MonoEight.Core.Physics;

/// <summary>
/// Provides helper methods for calculating if colliders are intersecting.
/// </summary>
public static class CollisionHelper
{
    /// <summary>
    /// Whether the given <see cref="CircleCollider"/> and <see cref="BoxCollider"/> intersect.
    /// </summary>
    /// <param name="circle">The <see cref="CircleCollider"/>.</param>
    /// <param name="box">The <see cref="BoxCollider"/>.</param>
    /// <returns><c>trye</c> if the given <see cref="Collider"/> intersect.</returns>
    public static bool CircleBox(CircleCollider circle, BoxCollider box)
    {
        float closestX = Math.Clamp(circle.Position.X, box.Min.X, box.Max.X);
        float closestY = Math.Clamp(circle.Position.Y, box.Min.Y, box.Max.Y);

        Vector2 closestPoint = new(closestX, closestY);
        float distanceSquared = Vector2.DistanceSquared(circle.Position, closestPoint);

        return distanceSquared < circle.Radius * circle.Radius;
    }

    /// <summary>
    /// Whether the given <see cref="Point"/> and <see cref="BoxCollider"/> intersect.
    /// </summary>
    /// <param name="point">The <see cref="Point"/>.</param>
    /// <param name="box">The <see cref="BoxCollider"/>.</param>
    /// <returns><c>true</c> if the given <see cref="Collider"/> and <see cref="Point"/> intersect.</returns>
    public static bool PointBox(Point point, BoxCollider box)
    {
        return point.X >= box.Min.X && point.X <= box.Max.X &&
               point.Y >= box.Min.Y && point.Y <= box.Max.Y;
    }

    /// <summary>
    /// Whether a given <see cref="Point"/> and <see cref="CircleCollider"/> intersect.
    /// </summary>
    /// <param name="point">The <see cref="Point"/>.</param>
    /// <param name="circle">The <see cref="CircleCollider"/>.</param>
    /// <returns><c>true</c> if the given <see cref="Collider"/> and <see cref="Point"/> intersect.</returns>
    public static bool PointCircle(Point point, CircleCollider circle)
    {
        float dx = point.X - circle.Position.X;
        float dy = point.Y - circle.Position.Y;
        float distanceSquared = dx * dx + dy * dy;
        return distanceSquared < circle.Radius * circle.Radius;
    }

    /// <summary>
    /// Whether the two given <see cref="CircleCollider">CircleColliders</see> intersect.
    /// </summary>
    /// <param name="a">The first <see cref="CircleCollider"/>.</param>
    /// <param name="b">The second <see cref="CircleCollider"/>.</param>
    /// <returns><c>true</c> if the given <see cref="CircleCollider">CircleColliders</see> intersect.</returns>
    public static bool CircleCircle(CircleCollider a, CircleCollider b)
    {
        float dx = a.Position.X - b.Position.X;
        float dy = a.Position.Y - b.Position.Y;
        float distanceSquared = dx * dx + dy * dy;
        float radiusSum = a.Radius + b.Radius;
        return distanceSquared < radiusSum * radiusSum;
    }

    /// <summary>
    /// Whether the two given <see cref="BoxCollider">BoxColliders</see> intersect.
    /// </summary>
    /// <param name="a">The first <see cref="BoxCollider"/>.</param>
    /// <param name="b">The second <see cref="BoxCollider"/>.</param>
    /// <returns><c>true</c> if the given <see cref="BoxCollider">BoxColliders</see> intersect.</returns>
    public static bool BoxBox(BoxCollider a, BoxCollider b)
    {
        return a.Min.X < b.Max.X &&
               a.Max.X > b.Min.X &&
               a.Min.Y < b.Max.Y &&
               a.Max.Y > b.Min.Y;
    }
}
