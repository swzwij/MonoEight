using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

/// <summary>
/// Base class for all game objects in the MonoEight engine.
/// This class provides basic properties and methods for game objects,
/// including position, rotation, layer, and active state.
/// Derived classes should implement the Update and Draw methods.
/// </summary>
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

    public GameObject(Vector2 position)
    {
        _transform = new Transform(position);
        _isActive = true;
        _layer = 0;
    }

    public GameObject()
    {
        _transform = new Transform();
        _isActive = true;
        _layer = 0;
    }

    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);

    public void Destroy()
    {
        _shouldDestroy = true;
    }
}