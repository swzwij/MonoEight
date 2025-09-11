using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public abstract class Scene
{
    private readonly string _name;
    private readonly List<GameObject> _gameObjects;

    public void Add(GameObject gameObject)
    {
        gameObject.Scene = this;
        _gameObjects.Add(gameObject);
    }

    public void Remove(GameObject gameObject)
    {
        gameObject.Scene = null;
        _gameObjects.Remove(gameObject);
    }

    public T Find<T>() where T : GameObject
    {
        return _gameObjects.OfType<T>().FirstOrDefault();
    }

    public T[] FindAll<T>() where T : GameObject
    {
        return _gameObjects.OfType<T>().ToArray();
    }

    public void Clear()
    {
        _gameObjects.Clear();
    }

    public void Update(GameTime gameTime)
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            if (!_gameObjects[i].ShouldDestroy)
                continue;

            _gameObjects.RemoveAt(i);
            i--;
        }

        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i].Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i].Draw(spriteBatch);
    }
}