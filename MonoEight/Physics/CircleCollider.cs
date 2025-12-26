using Microsoft.Xna.Framework;
using System;

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
            throw new NotImplementedException();
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
    }
}
