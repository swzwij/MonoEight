using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight.Core.Physics;

/// <summary>
/// An axis aligned rectangular <see cref="Collider"/>.
/// </summary>
public class BoxCollider : Collider
{
    /// <summary>
    /// Gets or sets the width and height.
    /// </summary>
    public Point Size { get; set; }

    /// <summary>
    /// Gets the top left corner of the box.
    /// </summary>
    public Vector2 Min => Position - Size.Float() / 2;
    
    /// <summary>
    /// Gets the bottom right corner of the box.
    /// </summary>
    public Vector2 Max => Position + Size.Float() / 2;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoxCollider"/> with the given size.
    /// </summary>
    /// <param name="size">The size of the collider.</param>
    public BoxCollider(Point size)
    {
        Size = size;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="size"><inheritdoc/></param>
    public BoxCollider(int size) : this(new Point(size)) { }
    
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="width">The width of the collider.</param>
    /// <param name="height">The height of the collider.</param>
    public BoxCollider(int width, int height) : this(new Point(width, height)) { }

    public override bool Intersects(Collider other)
    {
        return other.Intersects(this);
    }

    public override bool Intersects(Point point)
    {
        return CollisionHelper.PointBox(point, this);
    }

    public override bool Intersects(BoxCollider other)
    {
        return CollisionHelper.BoxBox(this, other);
    }

    public override bool Intersects(CircleCollider other)
    {
        return CollisionHelper.CircleBox(other, this);
    }

    protected override void Draw(SpriteBatch spriteBatch)
    {
        Debugger.DrawSquare(spriteBatch, Position.Int(), Size, Color.Green);
    }
}
