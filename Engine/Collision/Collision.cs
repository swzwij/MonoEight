namespace MonoEight;

/// <summary>
/// Collision detection class for 2D game objects.
/// </summary>
public static class Collision
{
    /// <summary>
    /// Checks if a circle collider intersects with a box collider.
    /// </summary>
    public static bool Intersects(CircleCollider circle, BoxCollider box)
    {
        return circle.Intersects(box);
    }

    /// <summary>
    /// Checks if a box collider intersects with a circle collider.
    /// </summary>
    public static bool Intersects(BoxCollider box, CircleCollider circle)
    {
        return circle.Intersects(box);
    }

    /// <summary>
    /// Checks if two circle colliders intersect.
    /// </summary>
    public static bool Intersects(CircleCollider circle1, CircleCollider circle2)
    {
        return circle1.Intersects(circle2);
    }

    /// <summary>
    /// Checks if two box colliders intersect.
    /// </summary>
    public static bool Intersects(BoxCollider box1, BoxCollider box2)
    {
        return box1.Intersects(box2);
    }
}