using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public abstract class GameState
{
    protected Scene _scene;

    public Scene Scene => _scene;

    public GameState()
    {
        _scene = new Scene();
    }

    public virtual void Initialize() { }
    public virtual void LoadContent() { }
    public virtual void UnloadContent() { }

    public virtual void Update(GameTime gameTime)
    {
        _scene.Update(gameTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        _scene.Draw(spriteBatch);
    }

    public void Add(GameObject gameObject)
    {
        _scene.Add(gameObject);
    }

    public void Remove(GameObject gameObject)
    {
        _scene.Remove(gameObject);
    }
}