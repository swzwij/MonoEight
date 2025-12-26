using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

/// <summary>
/// Represents an entity within a scene that can contain components and participate in the game loop lifecycle.
/// </summary>
/// <remarks>
/// A GameObject serves as a container for components, enabling modular behavior and logic composition.
/// It manages its own lifecycle, including initialization, content loading, updating, and drawing, and delegates these
/// phases to its components. GameObjects are typically managed by a Scene and can be activated, deactivated, or marked
/// for destruction. Components can be added or retrieved at runtime to extend functionality. GameObject implements
/// IDisposable to ensure proper cleanup of its components.
/// </remarks>
public class GameObject : IDisposable
{
    private readonly List<Component> _components = [];

    public Vector2 Position { get; set; }
    public bool IsActive { get; set; } = true;
    public bool ShouldDestroy { get; private set; }
    public Scene Scene { get; internal set; }

    public Canvas Canvas => Scene?.Canvas;

    protected virtual void Initialize() { }
    protected virtual void LoadContent() { }
    protected virtual void Update() { }
    protected virtual void Draw(SpriteBatch spriteBatch) { }

    public void InternalInitialize()
    {
        Initialize();

        for (int i = 0; i < _components.Count; i++)
            _components[i].InternalInitialize();
    }

    public void InternalLoadContent()
    {
        LoadContent();

        for (int i = 0; i < _components.Count; i++)
            _components[i].InternalLoadContent();
    }

    public void InternalUpdate()
    {
        if (!IsActive)
            return;

        Update();

        RemoveComponents();

        for (int i = 0; i < _components.Count; i++)
        {
            if (_components[i].IsActive)
                _components[i].InternalUpdate();
        }
    }

    public void InternalDraw(SpriteBatch spriteBatch)
    {
        if (!IsActive)
            return;

        Draw(spriteBatch);

        for (int i = 0; i < _components.Count; i++)
        {
            if (_components[i].IsActive)
                _components[i].InternalDraw(spriteBatch);
        }
    }

    public void AddComponent(Component component)
    {
        component.GameObject = this;
        _components.Add(component);

        if (component.GetType() == typeof(BoxCollider))
            Scene?.AddCollider(component as BoxCollider);
    }

    public T GetComponent<T>() where T : Component
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