using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight.Core.Physics;

/// <summary>
/// A circular <see cref="Collider"/>.
/// </summary>
public class CircleCollider : Collider
{
    /// <summary>
    /// Gets or sets the radius.
    /// </summary>
    public int Radius { get; set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CircleCollider"/> with the given radius.
    /// </summary>
    /// <param name="radius">The radius of the collider.</param>
    public CircleCollider(int radius)
    {
        Radius = radius;
    }

    public override bool Intersects(Collider other)
    {
        return other.Intersects(this);
    }

    public override bool Intersects(Point point)
    {
        return CollisionHelper.PointCircle(point, this);
    }

    public override bool Intersects(BoxCollider other)
    {
        return CollisionHelper.CircleBox(this, other);
    }

    public override bool Intersects(CircleCollider other)
    {
        return CollisionHelper.CircleCircle(this, other);
    }

    protected override void Draw(SpriteBatch spriteBatch)
    {
        Debugger.DrawCircle(spriteBatch, Position.Int(), Radius, Color.Green);
    }
}
