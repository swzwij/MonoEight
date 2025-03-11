namespace MonoEight;

public static class Collision
{
    public static bool Intersects(CircleCollider circle, BoxCollider box)
    {
        return circle.Intersects(box);
    }

    public static bool Intersects(BoxCollider box, CircleCollider circle)
    {
        return circle.Intersects(box);
    }

    public static bool Intersects(CircleCollider circle1, CircleCollider circle2)
    {
        return circle1.Intersects(circle2);
    }

    public static bool Intersects(BoxCollider box1, BoxCollider box2)
    {
        return box1.Intersects(box2);
    }
}