using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEight.Core.UI;
using MonoEight.Core.Physics;
using MonoEight.Core.Scenes;

namespace MonoEight.Core;

public class GameObject : IDisposable
{
    private readonly List<Component> _components = [];

    public Vector2 Position { get; set; }
    public bool IsActive { get; set; } = true;
    public bool ShouldDestroy { get; private set; }
    public Scene? Scene { get; internal set; }

    public Canvas? Canvas => Scene?.Canvas;

    protected virtual void Initialize() { }
    protected virtual void LoadContent() { }
    protected virtual void Update() { }
    protected virtual void Draw(SpriteBatch spriteBatch) { }

    public void InternalInitialize()
    {
        Initialize();

        foreach (Component component in _components)
            component.InternalInitialize();
    }

    public void InternalLoadContent()
    {
        LoadContent();

        foreach (Component component in _components)
            component.InternalLoadContent();
    }

    public void InternalUpdate()
    {
        if (!IsActive)
            return;

        Update();

        RemoveComponents();

        foreach (Component component in _components.Where(component => component.IsActive))
            component.InternalUpdate();
    }

    public void InternalDraw(SpriteBatch spriteBatch)
    {
        if (!IsActive)
            return;

        Draw(spriteBatch);

        foreach (Component component in _components.Where(component => component.IsActive))
            component.InternalDraw(spriteBatch);
    }

    public void AddComponent(Component component)
    {
        component.GameObject = this;
        _components.Add(component);

        if (component is Collider collider)
            Scene?.AddCollider(collider);
    }

    public T? GetComponent<T>() where T : Component
    {
        for (int i = 0; i < _components.Count; i++)
        {
            if (_components[i] is T component)
                return component;
        }
        return null;
    }

    public T[] GetComponents<T>() where T : Component
    {
        List<T> results = [];
        for (int i = 0; i < _components.Count; i++)
        {
            if (_components[i] is T component)
                results.Add(component);
        }
        return [.. results];
    }

    public void Destroy()
    {
        ShouldDestroy = true;
    }

    public virtual void Dispose()
    {
        for (int i = _components.Count - 1; i >= 0; i--)
        {
            _components[i].Dispose();
            _components[i].GameObject = null;
        }
        _components.Clear();
    }

    private void RemoveComponents()
    {
        for (int i = 0; i < _components.Count; i++)
        {
            if (!_components[i].ShouldDestroy)
                continue;

            RemoveComponent(i);
            _components.RemoveAt(i);
            i--;
        }
    }

    private void RemoveComponent(int index)
    {
        Component component = _components[index];
        component.Dispose();
        component.GameObject = null;
    }
}
