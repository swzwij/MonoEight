using Microsoft.Xna.Framework.Graphics;
using MonoEight.Core.Physics;
using MonoEight.Core.UI;

namespace MonoEight.Core.Scenes;

/// <summary>
/// Represents a logical container for game objects, camera, and UI elements within a game. Provides the base
/// functionality for managing the lifecycle and content of a game scene.
/// </summary>
/// <remarks>
/// Derive from this class to implement custom game scenes. Override the protected virtual methods to
/// define scene-specific initialization, content loading, updating, drawing, and unloading behavior. Scenes manage
/// their own collection of game objects and provide methods for adding and searching for components or game objects.
/// The scene's camera and canvas are accessible for rendering and UI purposes. Scene lifecycle methods are
/// called by the game engine and should not be invoked directly.
/// </remarks>
public abstract class Scene
{
    private readonly List<GameObject> _gameObjects = [];
    private readonly List<Collider> _colliders = [];

    public IReadOnlyList<Collider> Colliders => _colliders;

    public string Name { get; internal set; }
    public Camera Camera { get; set; } = new();
    public Canvas Canvas { get; private set; }

    protected virtual void Initialize() { }
    protected virtual void LoadContent() { }
    protected virtual void Update() { }
    protected virtual void Draw(SpriteBatch spriteBatch) { }
    protected virtual void Unload() { }

    public void InternalInitialize()
    {
        Initialize();

        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i].InternalInitialize();

        Canvas = new(this);
    }

    public void InternalLoadContent()
    {
        LoadContent();

        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i].InternalLoadContent();
    }

    public void InternalUpdate()
    {
        Update();

        RemoveObjects();

        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i].InternalUpdate();
    }

    public void InternalDraw(SpriteBatch spriteBatch)
    {
        Draw(spriteBatch);

        for (int i = 0; i < _gameObjects.Count; i++)
            _gameObjects[i].InternalDraw(spriteBatch);
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

    public GameObject Add()
    {
        GameObject gameObject = new();
        gameObject.Scene = this;
        _gameObjects.Add(gameObject);
        return gameObject;
    }

    public void Add(GameObject gameObject)
    {
        gameObject.Scene = this;
        _gameObjects.Add(gameObject);
    }

    public void AddCollider(Collider collider)
    {
        _colliders.Add(collider);
    }

    public T Find<T>() where T : Component
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            T component = _gameObjects[i].GetComponent<T>();
            if (component != null)
                return component;
        }
        return null;
    }

    public T[] FindAll<T>() where T : Component
    {
        List<T> results = [];
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            T[] components = _gameObjects[i].GetComponents<T>();
            results.AddRange(components);
        }
        return [.. results];
    }

    public GameObject FindGameObject<T>() where T : Component
    {
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            if (_gameObjects[i].GetComponent<T>() != null)
                return _gameObjects[i];
        }
        return null;
    }

    public GameObject[] FindGameObjects<T>() where T : Component
    {
        List<GameObject> results = [];
        for (int i = 0; i < _gameObjects.Count; i++)
        {
            if (_gameObjects[i].GetComponent<T>() != null)
                results.Add(_gameObjects[i]);
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
