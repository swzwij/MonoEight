using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

/// <summary>
/// Represents a scene in the game, containing a collection of game objects.
/// Provides methods to add, remove, find, update, and draw game objects.
/// </summary>
public class Scene
{
    private List<GameObject> _gameObjects;

    public Scene()
    {
        _gameObjects = [];
    }

    /// <summary>
    /// Adds a game object to the scene.
    /// The game object will be updated and drawn when the scene is updated and drawn.
    /// </summary>
    /// <param name="gameObject">The game object to add.</param>
    public void Add(GameObject gameObject)
    {
        _gameObjects.Add(gameObject);
    }

    /// <summary>
    /// Removes a game object from the scene.
    /// The game object will no longer be updated or drawn when the scene is updated and drawn.
    /// </summary>
    /// <param name="gameObject">The game object to remove.</param>
    public void Remove(GameObject gameObject)
    {
        _gameObjects.Remove(gameObject);
    }

    /// <summary>
    /// Finds a game object of the specified type in the scene.
    /// If multiple game objects of the same type exist, only the first one will be returned.
    /// </summary>
    /// <typeparam name="T">The type of game object to find.</typeparam>
    /// <returns>The first game object of the specified type, or null if none is found.</returns>
    public T Find<T>() where T : GameObject
    {
        return _gameObjects.OfType<T>().FirstOrDefault();
    }

    /// <summary>
    /// Finds all game objects of the specified type in the scene.
    /// </summary>
    /// <typeparam name="T">The type of game object to find.</typeparam>
    /// <returns>An array of all game objects of the specified type.</returns>
    public T[] FindAll<T>() where T : GameObject
    {
        return _gameObjects.OfType<T>().ToArray();
    }

    /// <summary>
    /// Clears all game objects from the scene.
    /// This method is useful for resetting the scene or removing all game objects at once.
    /// </summary>
    public void Clear()
    {
        _gameObjects.Clear();
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
}