using Microsoft.Xna.Framework.Graphics;

namespace MonoEight.Core.Scenes;

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

        _activeScene?.InternalUnload();

        _activeScene = scene;

        _activeScene.InternalInitialize();
        _activeScene.InternalLoadContent();
    }

    public static void Update()
    {
        _activeScene?.InternalUpdate();
    }

    public static void Draw(SpriteBatch? spriteBatch)
    {
        _activeScene?.InternalDraw(spriteBatch);
    }
}
