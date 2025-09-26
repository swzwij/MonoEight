using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoEight.Internal;

namespace MonoEight;

public class GameObject : MessageReceiver
{
    protected readonly List<Component> _components = [];

    public Vector2 Position { get; set; }
    public bool IsActive { get; set; }
    public bool ShouldDestroy { get; set; }
    public Scene Scene { get; set; }

    public void Add(Component component)
    {
        _components.Add(component);
    }

    public void Remove(Component component)
    {
        _components.Remove(component);
    }

    public void MessageComponents(string message, params object[] parameters)
    {
        int l = _components.Count;
        for (int i = 0; i < l; i++)
        {
            if (!_components[i].IsActive)
                continue;

            _components[i].SendMessage(message, parameters);
        }
    }

    public virtual void Destroy()
    {
        ShouldDestroy = true;
    }
}