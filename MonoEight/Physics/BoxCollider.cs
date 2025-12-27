using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class BoxCollider : Collider
{
    public Point Size { get; set; }

    public Vector2 Min => Position - (Size.Float() / 2);
    public Vector2 Max => Position + (Size.Float() / 2);

    public BoxCollider(Point size)
    {
        Size = size;
    }

    public BoxCollider(int size) : this(new Point(size)) { }
    
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

    //public bool Intersects(SquareCollider other)
    //{
    //    Point posA = (Position - (Size.Float() / 2)).Int();
    //    Point posB = (other.Position - (other.Size.Float() / 2)).Int();

    //    return posA.X < posB.X + other.Size.X &&
    //           posA.X + Size.X > posB.X &&
    //           posA.Y < posB.Y + other.Size.Y &&
    //           posA.Y + Size.Y > posB.Y;
    //}

    //public bool Intersects(Point point)
    //{
    //    Point posA = (Position - (Size.Float() / 2)).Int();
    //    return point.X >= posA.X &&
    //           point.X <= posA.X + Size.X &&
    //           point.Y >= posA.Y &&
    //           point.Y <= posA.Y + Size.Y;
    //}
}