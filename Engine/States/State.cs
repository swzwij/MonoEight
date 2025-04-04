using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

/// <summary>
/// Represents a base class for game states, providing methods for initialization, content loading, updating, and drawing.
/// Derived classes should implement specific game state logic.
/// </summary>
public abstract class State
{
    protected Scene _scene;

    public Scene Scene => _scene;

    /// <summary>
    /// Initializes a new instance of the <see cref="State"/> class.
    /// This constructor initializes the scene for the state.
    /// </summary>
    public State()
    {
        _scene = new Scene();
    }

    /// <summary>
    /// Initializes the state. This method can be overridden by derived classes to provide custom initialization logic.
    /// </summary>
    public virtual void Initialize() { }

    /// <summary>
    /// Loads the content for the state. This method can be overridden by derived classes to provide custom content loading logic.
    /// </summary>
    public virtual void LoadContent() { }

    /// <summary>
    /// Unloads the content for the state. This method can be overridden by derived classes to provide custom content unloading logic.
    /// </summary>
    public virtual void UnloadContent()
    {
        Clear();
    }

    /// <summary>
    /// Updates the state. This method can be overridden by derived classes to provide custom update logic.
    /// </summary>
    /// <param name="gameTime">The game time information.</param>
    public virtual void Update(GameTime gameTime)
    {
        _scene.Update(gameTime);
    }

    /// <summary>
    /// Draws the state. This method can be overridden by derived classes to provide custom drawing logic.
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used for drawing.</param>
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        _scene.Draw(spriteBatch);
    }

    /// <summary>
    /// Adds a game object to the scene.
    /// </summary>
    /// <param name="gameObject">The game object to add.</param>
    public void Add(GameObject gameObject)
    {
        _scene.Add(gameObject);
    }

    /// <summary>
    /// Removes a game object from the scene.
    /// </summary>
    public void Remove(GameObject gameObject)
    {
        _scene.Remove(gameObject);
    }

    /// <summary>
    /// Clears all game objects from the scene.
    /// </summary>
    public void Clear()
    {
        _scene.Clear();
    }
}