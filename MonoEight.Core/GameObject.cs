using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEight.Core.UI;
using MonoEight.Core.Physics;
using MonoEight.Core.Scenes;

namespace MonoEight.Core;

/// <summary>
/// Represents the base gameobject in the game world. 
/// Acts as a container for <see cref="Component"/>s and handles their lifecycle.
/// </summary>
public class GameObject : IDisposable
{
    private readonly List<Component> _components = [];

    /// <summary>
    /// Gets or sets the world space position of the object.
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// Gets or sets whether the object is active.
    /// If <c>false</c>, the object (and its components) will skip <see cref="Update"/> and <see cref="Draw"/>.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets whether the object is marked for destruction at the end of the frame.
    /// </summary>
    public bool ShouldDestroy { get; private set; }

    /// <summary>
    /// Gets the <see cref="Scene"/> that owns this object.
    /// </summary>
    public Scene? Scene { get; internal set; }

    /// <summary>
    /// Gets the UI <see cref="Canvas"/> associated with the current scene.
    /// </summary>
    public Canvas? Canvas => Scene?.Canvas;

    /// <summary>
    /// Called when the object is initialized. Override to set up initial state.
    /// </summary>
    protected virtual void Initialize() { }

    /// <summary>
    /// Called when content needs to be loaded. Override to load assets.
    /// </summary>
    protected virtual void LoadContent() { }

    /// <summary>
    /// Called once per frame to update logic.
    /// </summary>
    protected virtual void Update() { }

    /// <summary>
    /// Called once per frame to render the object.
    /// </summary>
    /// <param name="spriteBatch"><see cref="SpriteBatch"/></param>
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

    /// <summary>
    /// Attaches a new <see cref="Component"/> to this object.
    /// </summary>
    /// <remarks>
    /// Side Effect: If the component is a <see cref="Collider"/>, it is automatically registered 
    /// with the <see cref="Scene"/>'s physics system.
    /// </remarks>
    /// <param name="component">The component instance to add.</param>
    public void AddComponent(Component component)
    {
        component.GameObject = this;
        _components.Add(component);

        if (component is Collider collider)
            Scene?.AddCollider(collider);
    }
    
    /// <summary>
    /// Creates a new component of type T, attaches it to the object, and returns it.
    /// </summary>
    public T AddComponent<T>() where T : Component, new()
    {
        T component = new T();
        AddComponent(component);
        return component;
    }

    /// <summary>
    /// Retrieves the first component of the given type.
    /// </summary>
    /// <typeparam name="T">The type of component to find.</typeparam>
    /// <returns>The component instance, or <c>null</c> if not found.</returns>
    public T? GetComponent<T>() where T : Component
    {
        for (int i = 0; i < _components.Count; i++)
        {
            if (_components[i] is T component)
                return component;
        }
        return null;
    }

    /// <summary>
    /// Retrieves all components of the given type.
    /// </summary>
    /// <typeparam name="T">The type of components to find.</typeparam>
    /// <returns>An array containing all matching components.</returns>
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

    /// <summary>
    /// Marks the object to be removed from the scene at the end of the current update cycle.
    /// </summary>
    public void Destroy()
    {
        ShouldDestroy = true;
    }

    /// <summary>
    /// Disposes all attached components and clears references.
    /// </summary>
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
