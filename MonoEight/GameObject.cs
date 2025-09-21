using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEight.Internal;

namespace MonoEight;

public abstract class GameObject : MessageReceiver
{
    private readonly List<IComponent> _components = [];

    public Vector2 Position { get; set; }
    public bool IsActive { get; set; }
    public bool ShouldDestroy { get; set; }
    public Scene Scene { get; set; }

    public void AwakeComponents()
    {
        int l = _components.Count;
        for (int i = 0; i < l; i++)
        {
            if (!_components[i].IsActive)
                continue;

            _components[i].Awake();
        }
    }

    public void UpdateComponents()
    {
        int l = _components.Count;
        for (int i = 0; i < l; i++)
        {
            if (!_components[i].IsActive)
                continue;

            _components[i].Update();
        }
    }

    public void DrawComponents(SpriteBatch spriteBatch)
    {
        int l = _components.Count;
        for (int i = 0; i < l; i++)
        {
            if (!_components[i].IsActive)
                continue;

            _components[i].Draw(spriteBatch);
        }
    }

    public virtual void Destroy()
    {
        ShouldDestroy = true;
    }

    public void Add(IComponent component)
    {
        _components.Add(component);
    }

    public void Remove(IComponent component)
    {
        _components.Remove(component);
    }
}