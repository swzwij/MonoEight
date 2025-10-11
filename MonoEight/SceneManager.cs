using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public static class SceneManager
{
    private static readonly Dictionary<string, Scene> _scenes = [];
    private static Scene _activeScene;

    public static Scene ActiveScene => _activeScene;

    public static void Add(string name, Scene scene)
    {
        scene.Name = name;
        if (_scenes.TryAdd(name, scene))
            return;

        throw new ArgumentException($"Scene '{scene}' already exists in the scene manager");
    }

    public static void Load(string name)
    {
        if (!_scenes.TryGetValue(name, out Scene scene))
            throw new ArgumentException($"Scene '{name}' does not exist in the manager");

        _activeScene?.SendMessage("Unload");

        _activeScene = scene;

        _activeScene.SendMessage("Initialize");
        _activeScene.SendMessage("LoadContent");
    }

    public static void Update()
    {
        _activeScene?.SendMessage("Update");
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        _activeScene?.SendMessage("Draw", spriteBatch);
    }
}