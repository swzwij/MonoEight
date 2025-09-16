using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class SquareCollider
{
    public Vector2 Position { get; set; }
    public Point Size { get; set; }

    public SquareCollider(Vector2 position, Point size)
    {
        Position = position;
        Size = size;
    }

    public void Update(Vector2 position)
    {
        Position = position;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Debugger.DrawSquare(spriteBatch, Position.Int(), Size, Color.Green);
    }

    public bool Intersects(SquareCollider other)
    {
        Point posA = (Position - (Size.Float() / 2)).Int();
        Point posB = (other.Position - (other.Size.Float() / 2)).Int();

        return posA.X < posB.X + other.Size.X &&
               posA.X + Size.X > posB.X &&
               posA.Y < posB.Y + other.Size.Y &&
               posA.Y + Size.Y > posB.Y;
    }
}