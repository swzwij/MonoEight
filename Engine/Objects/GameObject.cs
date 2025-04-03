using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public abstract class GameObject
{
    protected Transform _transform;
    protected bool _isActive;
    protected int _layer;
    protected bool _shouldDestroy;
    protected Scene _scene;

    public Transform Transform => _transform;
    public bool IsActive => _isActive;
    public int Layer => _layer;
    public bool ShouldDestroy => _shouldDestroy;
    public Scene Scene
    {
        get => _scene;
        set => _scene = value;
    }

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