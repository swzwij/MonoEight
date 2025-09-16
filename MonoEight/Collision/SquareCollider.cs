using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class SquareCollider
{
    public Point Position { get; set; }
    public Point Size { get; set; }

    public SquareCollider(Point position, Point size)
    {
        Position = position;
        Size = size;
    }

    public void Update(Point position)
    {
        Position = position;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Debugger.DrawSquare(spriteBatch, Position, Size, Color.Green);
    }

    public bool Intersects(SquareCollider other)
    {
        Point posA = Position - (Size.ToVector2() / 2).ToPoint();
        Point posB = other.Position - (other.Size.ToVector2() / 2).ToPoint();

        return posA.X < posB.X + other.Size.X &&
               posA.X + Size.X > posB.X &&
               posA.Y < posB.Y + other.Size.Y &&
               posA.Y + Size.Y > posB.Y;
    }
}