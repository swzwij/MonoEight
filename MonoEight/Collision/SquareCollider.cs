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
        return Position.X < other.Position.X + other.Size.X &&
               Position.X + Size.X > other.Position.X &&
               Position.Y < other.Position.Y + other.Size.Y &&
               Position.Y + Size.Y > other.Position.Y;
    }
}