using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

/// <summary>
/// Represents a square-shaped collider component used for 2D collision detection in a scene.
/// </summary>
public class SquareCollider : Component
{
    public Point Size { get; set; }

    public bool IsColliding { get; set; }
    public bool WasColliding { get; set; }

    public Action OnCollisionEnter;
    public Action OnCollisionExit;
    public Action OnCollisionStay;

    public SquareCollider(Point size)
    {
        Size = size;
    }

    public SquareCollider(int width, int height) : this(new Point(width, height)) { }

    public SquareCollider(int size) : this(new Point(size)) { }

    protected override void Draw(SpriteBatch spriteBatch)
    {
        Color color = IsColliding ? Color.Red : Color.Green;
        Debugger.DrawSquare(spriteBatch, Position.Int(), Size, color);
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

    public bool Intersects(Point point)
    {
        Point posA = (Position - (Size.Float() / 2)).Int();
        return point.X >= posA.X &&
               point.X <= posA.X + Size.X &&
               point.Y >= posA.Y &&
               point.Y <= posA.Y + Size.Y;
    }

    public void UpdateState()
    {
        if (IsColliding && !WasColliding)
            OnCollisionEnter?.Invoke();
        else if (!IsColliding && WasColliding)
            OnCollisionExit?.Invoke();
        else if (IsColliding && WasColliding)
            OnCollisionStay?.Invoke();
    }
}