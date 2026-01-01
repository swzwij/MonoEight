using Microsoft.Xna.Framework;

namespace MonoEight.Core
{
    public abstract class Collider : Component
    {
        public abstract bool Intersects(Collider other);

        public abstract bool Intersects(Point point);
        public abstract bool Intersects(BoxCollider other);
        public abstract bool Intersects(CircleCollider other);
    }
}
