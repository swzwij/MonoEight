using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class SquareCollider
{
    public Vector2 Position { get; set; }
    public Point Size { get; set; }
    public bool IsActive { get; set; } = true;

    public bool IsColliding { get; set; }
    public bool WasColliding { get; set; }

    public Action OnCollisionEnter;
    public Action OnCollisionExit;
    public Action OnCollisionStay;

    public SquareCollider(Scene scene, Vector2 position, Point size)
    {
        scene.Add(this);

        Position = position;
        Size = size;
    }

    public SquareCollider(Scene scene, Vector2 position, int size) : this(scene, position, new Point(size)) { }

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