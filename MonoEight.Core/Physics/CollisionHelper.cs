using Microsoft.Xna.Framework;

namespace MonoEight.Core
{
    public static class CollisionHelper
    {
        public static bool CircleBox(CircleCollider circle, BoxCollider box)
        {
            float closestX = Math.Clamp(circle.Position.X, box.Min.X, box.Max.X);
            float closestY = Math.Clamp(circle.Position.Y, box.Min.Y, box.Max.Y);

            Vector2 closestPoint = new(closestX, closestY);
            float distanceSquared = Vector2.DistanceSquared(circle.Position, closestPoint);

            return distanceSquared < circle.Radius * circle.Radius;
        }

        public static bool PointBox(Point point, BoxCollider box)
        {
            return point.X >= box.Min.X && point.X <= box.Max.X &&
                   point.Y >= box.Min.Y && point.Y <= box.Max.Y;
        }

        public static bool PointCircle(Point point, CircleCollider circle)
        {
            float dx = point.X - circle.Position.X;
            float dy = point.Y - circle.Position.Y;
            float distanceSquared = dx * dx + dy * dy;
            return distanceSquared < circle.Radius * circle.Radius;
        }

        public static bool CircleCircle(CircleCollider a, CircleCollider b)
        {
            float dx = a.Position.X - b.Position.X;
            float dy = a.Position.Y - b.Position.Y;
            float distanceSquared = dx * dx + dy * dy;
            float radiusSum = a.Radius + b.Radius;
            return distanceSquared < radiusSum * radiusSum;
        }

        public static bool BoxBox(BoxCollider a, BoxCollider b)
        {
            return a.Min.X < b.Max.X &&
                   a.Max.X > b.Min.X &&
                   a.Min.Y < b.Max.Y &&
                   a.Max.Y > b.Min.Y;
        }
    }
}
