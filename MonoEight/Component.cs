using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

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