using Microsoft.Xna.Framework;

namespace MonoEight.Core.Physics;

/// <summary>
/// A base collider class to handle intersection.
/// </summary>
public abstract class Collider : Component
{
    /// <summary>
    /// Whether this collider intersects with the other given collider. 
    /// </summary>
    /// <param name="other">The other collider used to check intersection.</param>
    /// <returns><c>true</c> if the colliders intersect.</returns>
    public abstract bool Intersects(Collider other);
    
    /// <summary>
    /// Whether this collider intersects with the given <see cref="Point"/>.
    /// </summary>
    /// <param name="point">The <see cref="Point"/> to check against.</param>
    /// <returns><c>true</c> if the collider intersect with the given <see cref="Point"/>.</returns>
    public abstract bool Intersects(Point point);
    
    /// <summary>
    /// Whether this collider intersects with the given <see cref="BoxCollider"/>.
    /// </summary>
    /// <param name="other">The <see cref="BoxCollider"/> to check against.</param>
    /// <returns><c>true</c> if the collider intersect with the given <see cref="BoxCollider"/>.</returns>
    public abstract bool Intersects(BoxCollider other);
    
    /// <summary>
    /// Whether this collider intersects with the given <see cref="CircleCollider"/>.
    /// </summary>
    /// <param name="other">The <see cref="CircleCollider"/> to check against.</param>
    /// <returns><c>true</c> if the collider intersect with the given <see cref="CircleCollider"/>.</returns>
    public abstract bool Intersects(CircleCollider other);
}
