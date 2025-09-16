using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public abstract class GameObject
{
    public Vector2 Position { get; set; }
    public bool IsActive { get; set; }
    public bool ShouldDestroy { get; set; }
    public Scene Scene { get; set; }

    public virtual void Update(GameTime gameTime) { }
    public virtual void Draw(SpriteBatch spriteBatch) { }

    public virtual void Destroy()
    {
        ShouldDestroy = true;
    }
}