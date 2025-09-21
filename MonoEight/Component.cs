using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public interface IComponent
{
    public GameObject GameObject { get; protected set; }
    public bool IsActive { get; set; }

    public void Awake();
    public void Update();
    public void Draw(SpriteBatch spriteBatch);
}

// public abstract class Component
// {
//     public GameObject GameObject { get; protected set; }
//     public bool IsActive { get; set; }

//     public virtual
// }