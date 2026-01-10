using Microsoft.Xna.Framework.Graphics;
using MonoEight.Core.Physics;
using MonoEight.Core.UI;

namespace MonoEight.Core.Scenes;

/// <summary>
/// Represents an abstract base class for a game scene or level.
/// Manages the lifecycle and rendering for all <see cref="GameObject">GameObjects</see> contained within it.
/// </summary>
public abstract class Scene
{
    private readonly List<GameObject> _gameObjects = [];
    private readonly List<Collider> _colliders = [];

    /// <summary>
    /// A list of all the colliders in the <see cref="Scene"/>.
    /// </summary>
    public IReadOnlyList<Collider> Colliders => _colliders;

    /// <summary>
    /// The name of the <see cref="Scene"/>.
    /// </summary>
    public string Name { get; internal set; } = null!;

    /// <summary>
    /// The <see cref="Camera"/> of the <see cref="Scene"/>.
    /// </summary>
    public Camera Camera { get; set; } = new();
    
    /// <summary>
    /// The <see cref="Canvas"/> of the <see cref="Scene"/>.
    /// </summary>
    public Canvas? Canvas { get; private set; }

    protected virtual void Initialize() { }
    protected virtual void LoadContent() { }
    protected virtual void Update() { }
    protected virtual void Draw(SpriteBatch spriteBatch) { }
    protected virtual void Unload() { }

    public void InternalInitialize()
    {
        Initialize();

        foreach (GameObject gameObject in _gameObjects)
            gameObject.InternalInitialize();

        Canvas = new Canvas(this);
    }

    public void InternalLoadContent()
    {
        LoadContent();

        foreach (GameObject gameObject in _gameObjects)
            gameObject.InternalLoadContent();
    }

    public void InternalUpdate()
    {
        Update();

        RemoveObjects();

        foreach (GameObject gameObject in _gameObjects)
            gameObject.InternalUpdate();
    }

    public void InternalDraw(SpriteBatch spriteBatch)
    {
        Draw(spriteBatch);

        foreach (GameObject gameObject in _gameObjects)
            gameObject.InternalDraw(spriteBatch);
    }

    public void InternalUnload()
    {
        Unload();

        for (int i = _gameObjects.Count - 1; i >= 0; i--)
        {
            _gameObjects[i].Dispose();
            _gameObjects[i].Scene = null;
        }

        _gameObjects.Clear();
        Canvas = null;
    }

    /// <summary>
    /// Adds a empt<see cref="GameObject"/> to the <see cref="Scene"/>.
    /// </summary>
    /// <returns>The added <see cref="GameObject"/>.</returns>
    public GameObject Add()
    {
        GameObject gameObject = new();
        gameObject.Scene = this;
        _gameObjects.Add(gameObject);
        return gameObject;
    }

    /// <summary>
    /// Adds a <see cref="GameObject"/> to the <see cref="Scene"/>.
    /// </summary>
    /// <param name="gameObject">The <see cref="GameObject"/> that wil be added.</param>
    public void Add(GameObject gameObject)
    {
        gameObject.Scene = this;
        _gameObjects.Add(gameObject);
    }

    /// <summary>
    /// Adds a <see cref="Collider"/> to the <see cref="Scene"/>.
    /// </summary>
    /// <param name="collider">The <see cref="Collider"/> that wil be added.</param>
    public void AddCollider(Collider collider)
    {
        _colliders.Add(collider);
    }

    /// <summary>
    /// Finds the first <see cref="Component"/> of the given type.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Component"/> to find.</typeparam>
    /// <returns>The first <see cref="Component"/> of type <typeparamref name="T"/> found, or <c>null</c> if none exist.</returns>
    public T? Find<T>() where T : Component
    {
        return _gameObjects
            .Select(gameObject => gameObject.GetComponent<T>())
            .OfType<T>()
            .FirstOrDefault();
    }

    /// <summary>
    /// Finds all <see cref="Component">Components</see> of the given type.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Component">Components</see> to find.</typeparam>
    /// <returns>An array of <see cref="Component">Components</see> of type <typeparamref name="T"/>.</returns>
    public T[] FindAll<T>() where T : Component
    {
        List<T> results = [];
        foreach (T[] components in _gameObjects.Select(gameObject => gameObject.GetComponents<T>()))
            results.AddRange(components);
        return [.. results];
    }

    /// <summary>
    /// Finds the first <see cref="GameObject"/> of the given type.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="GameObject"/> to find.</typeparam>
    /// <returns>The first <see cref="GameObject"/> of type <typeparamref name="T"/> found, or <c>null</c> if none exist.</returns>
    public GameObject? FindGameObject<T>() where T : Component
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            if (gameObject.GetComponent<T>() == null)
                continue;
            return gameObject;
        }
        return null;
    }

    /// <summary>
    /// Finds all <see cref="GameObject">GameObjects</see> of the given type.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="GameObject">GameObjects</see> to find.</typeparam>
    /// <returns>An array of <see cref="GameObject">GameObjects</see> of type <typeparamref name="T"/>.</returns>
    public GameObject[] FindGameObjects<T>() where T : Component
    {
        List<GameObject> results = [];
        foreach (GameObject gameObject in _gameObjects)
        {
            if (gameObject.GetComponent<T>() == null)
                continue;
            results.Add(gameObject);
        }
        return [.. results];
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
