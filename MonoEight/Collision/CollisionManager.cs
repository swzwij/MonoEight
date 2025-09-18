using System.Collections.Generic;

namespace MonoEight;

public class CollisionManager
{
    public void Update(List<SquareCollider> colliders)
    {
        int l = colliders.Count;
        for (int i = 0; i < l; i++)
            UpdateCollider(colliders[i]);

        for (int i = 0; i < l; i++)
        {
            if (!colliders[i].IsActive)
                continue;

            for (int j = i + 1; j < l; j++)
                CheckCollision(colliders[i], colliders[j]);
        }

        for (int i = 0; i < l; i++)
        {
            if (colliders[i].IsActive)
                colliders[i].UpdateState();
        }
    }

    private void UpdateCollider(SquareCollider collider)
    {
        if (!collider.IsActive)
            return;

        collider.WasColliding = collider.IsColliding;
        collider.IsColliding = false;
    }

    private void CheckCollision(SquareCollider colliderA, SquareCollider colliderB)
    {
        if (!colliderB.IsActive)
            return;

        if (!colliderA.Intersects(colliderB))
            return;

        colliderA.IsColliding = true;
        colliderB.IsColliding = true;
    }
}