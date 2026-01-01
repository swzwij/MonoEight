using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight.Core;

/// <summary>
/// Represents a base class for attachable components that add behavior or data to a GameObject within a scene.
/// </summary>
/// <remarks>
/// Component instances are associated with a GameObject and participate in the game loop
/// through initialization, content loading, updating, and drawing. Derive from this class to implement custom
/// behaviors. Components can be enabled or disabled using the IsActive property, and can be scheduled for removal by
/// calling Destroy().
/// </remarks>
public class Component : IDisposable
{
    /// <summary>
    /// Gets or sets a value indicating whether the component is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets a value indicating whether the component is scheduled to be destroyed.
    /// </summary>
    public bool ShouldDestroy { get; private set; }

    /// <summary>
    /// Gets the associated GameObject instance.
    /// </summary>
    public GameObject GameObject { get; internal set; }

    /// <summary>
    /// Gets the current position of the associated game object.
    /// </summary>
    /// <remarks>
    /// If the game object is not set, the position defaults to <see cref="Vector2.Zero"/>. This
    /// property is read-only.
    /// </remarks>
    public Vector2 Position => GameObject?.Position ?? Vector2.Zero;

    /// <summary>
    /// Gets the scene to which the associated gameobject belongs.
    /// </summary>
    public Scene Scene => GameObject?.Scene;

    /// <summary>
    /// Performs initialization logic for the current instance. Called during the setup phase to allow derived classes
    /// to provide custom initialization behavior.
    /// </summary>
    /// <remarks>
    /// Override this method in a derived class to implement custom initialization steps. The base
    /// implementation does nothing.
    /// </remarks>
    protected virtual void Initialize() { }

    /// <summary>
    /// Allows derived classes to load any game specific content.
    /// </summary>
    /// <remarks>
    /// Override this method in a subclass to initialize or load resources such as textures, sounds,
    /// or other assets required by the game. This method is typically called once during the initialization phase of
    /// the game loop.
    /// </remarks>
    protected virtual void LoadContent() { }

    /// <summary>
    /// Performs an update operation. Override this method in a derived class to implement custom update logic.
    /// </summary>
    /// <remarks>
    /// This method is intended to be overridden in subclasses to provide specific update behavior.
    /// The base implementation does nothing.
    /// </remarks>
    protected virtual void Update() { }

    /// <summary>
    /// Draws the object using the specified sprite batch.
    /// </summary>
    /// <remarks>
    /// Override this method in a derived class to implement custom drawing logic. This method is
    /// typically called during the rendering phase of the game loop.
    /// </remarks>
    /// <param name="spriteBatch">The sprite batch used to render the object. Cannot be null.</param>
    protected virtual void Draw(SpriteBatch spriteBatch) { }

    public void InternalInitialize() => Initialize();
    public void InternalLoadContent() => LoadContent();
    public void InternalUpdate() => Update();
    public void InternalDraw(SpriteBatch spriteBatch) => Draw(spriteBatch);

    /// <summary>
    /// Marks the object for destruction, indicating that it should be removed or cleaned up by the system.
    /// </summary>
    public void Destroy()
    {
        ShouldDestroy = true;
    }

    /// <summary>
    /// Releases all resources used by the current instance of the class.
    /// </summary>
    /// <remarks>
    /// Call this method when you are finished using the object to release unmanaged resources and
    /// perform other cleanup operations. After calling Dispose, the object should not be used further.
    /// </remarks>
    public virtual void Dispose() { }
}
