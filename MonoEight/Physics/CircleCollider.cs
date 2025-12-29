using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight
{
    public class CircleCollider : Collider
    {
        public int Radius { get; set; }
        
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
}
