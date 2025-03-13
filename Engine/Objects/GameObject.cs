using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public abstract class GameObject
{
    protected Transform _transform;
    protected bool _isActive;
    protected int _layer;
    protected bool _shouldDestroy;

    public Transform Transform => _transform;
    public bool IsActive => _isActive;
    public int Layer => _layer;
    public bool ShouldDestroy => _shouldDestroy;

    public GameObject(Vector2 position, float rotation, int layer)
    {
        _transform = new Transform(position, rotation);
        _isActive = true;
        _layer = layer;
    }

    public GameObject(Vector2 position, int layer)
    {
        _transform = new Transform(position);
        _isActive = true;
        _layer = layer;
    }

    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);

    public void Destroy()
    {
        _shouldDestroy = true;
    }
}