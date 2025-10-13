using System;
using Microsoft.Xna.Framework;
using MonoEight.Internal;

namespace MonoEight;

public class Component : MessageReceiver, IDisposable
{
    public bool IsActive { get; set; } = true;
    public bool ShouldDestroy { get; private set; }
    public GameObject GameObject { get; internal set; }

    public Vector2 Position => GameObject?.Position ?? Vector2.Zero;
    public Scene Scene => GameObject?.Scene;

    public void Destroy()
    {
        ShouldDestroy = true;
    }

    public virtual void Dispose() { }
}