using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEight.Core.Scenes;

namespace MonoEight.Core;

/// <summary>
/// Represents the base class for all behaviors attached to a <see cref="GameObject"/>.
/// </summary>
/// <remarks>
/// Inherit from this class to create custom logic, rendering, or physics behaviors.
/// Components must be attached to a <see cref="MonoEight.Core.GameObject"/> to function.
/// </remarks>
public class Component : IDisposable
{
    /// <summary>
    /// Gets or sets whether this component is active.
    /// If <c>false</c>, <see cref="Update"/> and <see cref="Draw"/> will not be called.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets whether this component is marked for destruction at the end of the frame.
    /// </summary>
    public bool ShouldDestroy { get; private set; }

    /// <summary>
    /// Gets the <see cref="GameObject"/> that owns this component.
    /// </summary>
    public GameObject GameObject { get; internal set; } = null!;

    /// <summary>
    /// Gets the world position of the parent <see cref="GameObject"/>.
    /// </summary>
    public Vector2 Position => GameObject.Position;

    /// <summary>
    /// Gets the <see cref="Scene"/> that the parent <see cref="GameObject"/> belongs to.
    /// </summary>
    public Scene Scene => GameObject.Scene!;

    /// <summary>
    /// Called when the component is first initialized. 
    /// </summary>
    /// <remarks>
    /// Override this to set up initial state.
    /// </remarks>
    protected virtual void Initialize() { }

    /// <summary>
    /// Called when content needs to be loaded.
    /// </summary>
    /// <remarks>
    /// Override this to load textures, sounds, or other resources.
    /// </remarks>
    protected virtual void LoadContent() { }

    /// <summary>
    /// Called once per frame to update game logic.
    /// </summary>
    protected virtual void Update() { }

    /// <summary>
    /// Called once per frame to render the component.
    /// </summary>
    /// <param name="spriteBatch"><see cref="SpriteBatch"/></param>
    protected virtual void Draw(SpriteBatch spriteBatch) { }
    
    public void InternalInitialize() => Initialize();
    public void InternalLoadContent() => LoadContent();
    public void InternalUpdate() => Update();
    public void InternalDraw(SpriteBatch spriteBatch) => Draw(spriteBatch);

    /// <summary>
    /// Marks this component to be removed from its <see cref="GameObject"/> at the end of the current frame.
    /// </summary>
    public void Destroy()
    {
        ShouldDestroy = true;
    }

    /// <summary>
    /// Releases unmanaged resources used by the component.
    /// </summary>
    public virtual void Dispose() { }
}
