using System;
using Microsoft.Xna.Framework;

namespace MonoEight;

public class BoxCollider : Collider
{
    public Point Size { get; set; }

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
        throw new NotImplementedException();
    }

    public override bool Intersects(BoxCollider other)
    {
        throw new NotImplementedException();
    }

    public override bool Intersects(CircleCollider other)
    {
        throw new NotImplementedException();
    }

    //protected override void Draw(SpriteBatch spriteBatch)
    //{
    //    Color color = IsColliding ? Color.Red : Color.Green;
    //    Debugger.DrawSquare(spriteBatch, Position.Int(), Size, color);
    //}

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