using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

/// <summary>
/// Represents a base class for attachable components that add behavior or data to a GameObject within a scene.
/// </summary>
/// <remarks>
/// Component instances are typically associated with a GameObject and participate in the game loop
/// through initialization, content loading, updating, and drawing. Derive from this class to implement custom
/// behaviors. Components can be enabled or disabled using the IsActive property, and can be scheduled for removal by
/// calling Destroy().
/// </remarks>
public class Component : IDisposable
{
    public bool IsActive { get; set; } = true;
    public bool ShouldDestroy { get; private set; }
    public GameObject GameObject { get; internal set; }

    public Vector2 Position => GameObject?.Position ?? Vector2.Zero;
    public Scene Scene => GameObject?.Scene;

    protected virtual void Initialize() { }
    protected virtual void LoadContent() { }
    protected virtual void Update() { }
    protected virtual void Draw(SpriteBatch spriteBatch) { }

    public void InternalInitialize() => Initialize();
    public void InternalLoadContent() => LoadContent();
    public void InternalUpdate() => Update();
    public void InternalDraw(SpriteBatch spriteBatch) => Draw(spriteBatch);

    public void Destroy()
    {
        ShouldDestroy = true;
    }

    public virtual void Dispose() { }
}