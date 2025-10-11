using System;
using Microsoft.Xna.Framework;
using MonoEight.Internal;

namespace MonoEight;

public class GameObject : MessageReceiver, IDisposable
{
    public Vector2 Position { get; set; }
    public bool IsActive { get; set; }
    public bool ShouldDestroy { get; set; }
    public Scene Scene { get; set; }

    public void Dispose() { }

    public void Destroy()
    {
        ShouldDestroy = true;
    }
}