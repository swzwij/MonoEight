using Microsoft.Xna.Framework.Graphics;

namespace MonoEight.Core.Scenes;

/// <summary>
/// A static manager responsible for registering, storing, and switching between <see cref="Scene">Scenes</see>.
/// </summary>
public static class SceneManager
{
    private static readonly Dictionary<string, Scene> _scenes = [];

    /// <summary>
    /// The currently active <see cref="Scene"/>.
    /// </summary>
    public static Scene? ActiveScene { get; private set; }

    /// <summary>
    /// Adds a <see cref="Scene"/> to the manager.
    /// </summary>
    /// <param name="name">The name of the to be added <see cref="Scene"/>.</param>
    /// <param name="scene">The to be added <see cref="Scene"/>.</param>
    /// <exception cref="ArgumentException">Thrown if a <see cref="Scene"/> with the same <paramref name="name"/> already exists.</exception>
    public static void Add(string name, Scene scene)
    {
        scene.Name = name;
        
        if (_scenes.TryAdd(name, scene))
            return;

        throw new ArgumentException($"Scene '{scene}' already exists in the scene manager");
    }

    /// <summary>
    /// Load a given <see cref="Scene"/> by name.
    /// </summary>
    /// <param name="name">The name of the to be loaded <see cref="Scene"/>.</param>
    /// <exception cref="ArgumentException">Thrown if no <see cref="Scene"/> is found with the given <paramref name="name"/>.</exception>
    public static void Load(string name)
    {
        if (!_scenes.TryGetValue(name, out Scene? scene))
            throw new ArgumentException($"Scene '{name}' does not exist in the manager");

        ActiveScene?.InternalUnload();

        ActiveScene = scene;

        ActiveScene.InternalInitialize();
        ActiveScene.InternalLoadContent();
    }

    /// <summary>
    /// Updates the <see cref="ActiveScene"/>.
    /// </summary>
    public static void Update()
    {
        ActiveScene?.InternalUpdate();
    }

    /// <summary>
    /// Draws the <see cref="ActiveScene"/>.
    /// </summary>
    /// <param name="spriteBatch"><see cref="SpriteBatch"/></param>
    public static void Draw(SpriteBatch spriteBatch)
    {
        ActiveScene?.InternalDraw(spriteBatch);
    }
}
