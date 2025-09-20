using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public interface IComponent
{
    public GameObject GameObject { get; protected set; }
    public bool IsActive { get; set; }

    public void Awake();
    public void Update(GameTime gameTime);
    public void Draw(SpriteBatch spriteBatch);
}