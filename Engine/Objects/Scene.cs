using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class Scene
{
    private List<GameObject> _gameObjects;

    public Scene()
    {
        _gameObjects = [];
    }

    public void Add(GameObject gameObject)
    {
        _gameObjects.Add(gameObject);
    }

    public void Remove(GameObject gameObject)
    {
        _gameObjects.Remove(gameObject);
    }

    public void Update(GameTime gameTime)
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            if (_gameObjects[i].ShouldDestroy)
            {
                _gameObjects.RemoveAt(i);
                i--;
                continue;
            }

            _gameObjects[i].Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _gameObjects = _gameObjects.OrderBy(go => go.Layer).ToList();

        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i].Draw(spriteBatch);
    }

    public void Clear()
    {
        _gameObjects.Clear();
    }
}