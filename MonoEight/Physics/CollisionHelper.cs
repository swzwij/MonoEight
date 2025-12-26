using System;

namespace MonoEight
{
    public static class CollisionHelper
    {
        public static bool CircleBox(CircleCollider circle, BoxCollider box)
        {
            float closestX = Math.Clamp(circle.Position.X, box.Min.X, box.Max.X);
            float closestY = Math.Clamp(circle.Position.Y, box.Min.Y, box.Max.Y);

            Vector2 closestPoint = new Vector2(closestX, closestY);
            float distanceSquared = Vector2.DistanceSquared(circle.Position, closestPoint);

            return distanceSquared < (circle.Radius * circle.Radius);
        }
    }
}
