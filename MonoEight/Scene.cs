using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public abstract class Scene
{
    private readonly List<GameObject> _gameObjects = [];
    private readonly List<SquareCollider> _colliders = [];
    private readonly CollisionManager _collisionManager = new();

    public string Name { get; set; }
    public Camera Camera { get; set; } = new();
    public Canvas Canvas { get; private set; }

    public virtual void Awake()
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            _gameObjects[i].SendMessage("Awake");
            _gameObjects[i].AwakeComponents();
        }

        Canvas = new(this);
    }

    public virtual void LoadContent() { }

    public virtual void UnloadContent()
    {
        Clear();
    }

    public virtual void Update(GameTime gameTime)
    {
        RemoveObjects();
        UpdateObjects(gameTime);
        _collisionManager.Update(_colliders);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            _gameObjects[i].SendMessage("Draw", spriteBatch);
            _gameObjects[i].DrawComponents(spriteBatch);
        }
    }

    public void Add(GameObject gameObject)
    {
        gameObject.Scene = this;
        _gameObjects.Add(gameObject);
    }

    public void Add(SquareCollider collider)
    {
        _colliders.Add(collider);
    }

    public void Remove(GameObject gameObject)
    {
        gameObject.Scene = null;
        _gameObjects.Remove(gameObject);
    }

    public void Remove(SquareCollider collider)
    {
        _colliders.Remove(collider);
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
        for (int i = 0; i < _gameObjects.Count; i++)
            RemoveObject(i);

        _gameObjects.Clear();
        _colliders.Clear();

        Camera = new();
        Canvas = new(this);
    }

    private void RemoveObjects()
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            if (!_gameObjects[i].ShouldDestroy)
                continue;

            RemoveObject(i);
            _gameObjects.RemoveAt(i);
            i--;
        }
    }

    private void RemoveObject(int index)
    {
        GameObject gameObject = _gameObjects[index];

        if (gameObject is IDisposable disposable)
            disposable.Dispose();

        gameObject.Scene = null;
    }

    private void UpdateObjects(GameTime gameTime)
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            _gameObjects[i].SendMessage("Update");
            _gameObjects[i].UpdateComponents();
        }
    }
}