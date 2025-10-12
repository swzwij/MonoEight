using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using MonoEight.Internal;

namespace MonoEight;

public abstract class Scene : MessageReceiver
{
    private readonly List<GameObject> _gameObjects = [];

    public string Name { get; set; }
    public Camera Camera { get; set; } = new();
    public Canvas Canvas { get; private set; }

    public void InternalInitialize()
    {
        SendMessage("Initialize");

        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i].SendMessage("Initialize");

        Canvas = new(this);
    }

    public void InternalLoadContent()
    {
        SendMessage("LoadContent");

        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i].SendMessage("LoadContent");
    }

    public void InternalUpdate()
    {
        SendMessage("Update");

        RemoveObjects();

        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i].SendMessage("Update");
    }

    public void InternalDraw(SpriteBatch spriteBatch)
    {
        SendMessage("Draw", spriteBatch);

        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i].SendMessage("Draw", spriteBatch);
    }

    public void InternalUnload()
    {
        SendMessage("Unload");

        for (int i = _gameObjects.Count - 1; i >= 0; i--)
        {
            _gameObjects[i].Dispose();
            _gameObjects[i].Scene = null;
        }

        _gameObjects.Clear();
        Canvas = null;
    }

    public void Add(GameObject gameObject)
    {
        gameObject.Scene = this;
        _gameObjects.Add(gameObject);
    }

    public T Find<T>() where T : GameObject
    {
        return _gameObjects.OfType<T>().FirstOrDefault();
    }

    public T[] FindAll<T>() where T : GameObject
    {
        return _gameObjects.OfType<T>().ToArray();
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
        gameObject.Dispose();
        gameObject.Scene = null;
    }
}