using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoEight.Core.UserInput;

/// <summary>
/// A static manger that polls keyboard, gamepad, and mouse states to provide unified access. 
/// </summary>
public static class Input
{
    private static KeyboardState _keys;
    private static KeyboardState _lastKeys;
    private static GamePadState _buttons;
    private static GamePadState _lastButtons;
    private static readonly Dictionary<string, InputAction> _actions = [];

    /// <summary>
    /// Gets or sets the threshold of detecting analog stick movement. Threshold is between 0.0 and 1.0.
    /// Default is 0.1.
    /// </summary>
    public static float ControllerDeadZone { get; set; } = 0.1f;
    
    /// <summary>
    /// Gets the horizontal input axis based on the inputs of A/D, left/right arrows, DPad, and left stick.
    /// </summary>
    /// <returns>
    /// -1 for left and 1 for right.
    /// </returns>
    public static int HorizontalAxis
    {
        get
        {
            int axis = 0;

            axis -= Convert.ToInt32(IsKeyDown(Keys.Left));
            axis -= Convert.ToInt32(IsKeyDown(Keys.A));
            axis += Convert.ToInt32(IsKeyDown(Keys.Right));
            axis += Convert.ToInt32(IsKeyDown(Keys.D));

            axis += _buttons.ThumbSticks.Left.X > ControllerDeadZone ? 1 : 0;

            axis -= Convert.ToInt32(IsButtonDown(Buttons.DPadLeft));
            axis += Convert.ToInt32(IsButtonDown(Buttons.DPadRight));

            return Math.Clamp(axis, -1, 1);
        }
    }

    /// <summary>
    /// Gets the vertical input axis based on the inputs of W/S, up/down arrows, DPad, and left stick.
    /// </summary>
    /// <returns>
    /// -1 for up and 1 for down.
    /// </returns>
    public static int VerticalAxis
    {
        get
        {
            int axis = 0;

            axis -= Convert.ToInt32(IsKeyDown(Keys.Up));
            axis -= Convert.ToInt32(IsKeyDown(Keys.W));
            axis += Convert.ToInt32(IsKeyDown(Keys.Down));
            axis += Convert.ToInt32(IsKeyDown(Keys.S));

            axis += _buttons.ThumbSticks.Left.Y > ControllerDeadZone ? 1 : 0;

            axis -= Convert.ToInt32(IsButtonDown(Buttons.DPadUp));
            axis += Convert.ToInt32(IsButtonDown(Buttons.DPadDown));

            return Math.Clamp(axis, -1, 1);
        }
    }
    
    /// <summary>
    /// Gets the combined <see cref="HorizontalAxis"/> and <see cref="VerticalAxis"/> in a <see cref="Point"/>.
    /// </summary>
    public static Point InputAxis => new(HorizontalAxis, VerticalAxis);
    
    /// <summary>
    /// Gets the mouse handler instance for accessing the cursor positon and clicks.
    /// </summary>
    public static MouseHandler Mouse { get; } = new();

    /// <summary>
    /// Updates the current and previous input states. 
    /// </summary>
    /// <param name="displayRect">The current display rectangle.</param>
    public static void Update(Rectangle displayRect)
    {
        _lastKeys = _keys;
        _keys = Keyboard.GetState();
        _lastButtons = _buttons;
        _buttons = GamePad.GetState(PlayerIndex.One);

        Mouse.Update(displayRect);

        foreach (InputAction action in _actions.Values)
            action.Update(_keys, _lastKeys, _buttons, _lastButtons);
    }

    /// <summary>
    /// Checks if the given key is currently held down.
    /// </summary>
    /// <param name="key">The keyboard key to check.</param>
    /// <returns><c>true</c> if the key is down, otherwise, <c>false</c>.</returns>
    public static bool IsKeyDown(Keys key)
    {
        return _keys.IsKeyDown(key);
    }

    /// <summary>
    /// Checks if the given key was pressed this frame.
    /// </summary>
    /// <remarks>
    /// Returns <c>true</c> only on the single frame the key transitions from Up to Down.
    /// </remarks>
    public static bool IsKeyPressed(Keys key)
    {
        return _keys.IsKeyDown(key) && !_lastKeys.IsKeyDown(key);
    }

    /// <summary>
    /// Checks if the given key was released this frame.
    /// </summary>
    /// <remarks>
    /// Returns <c>true</c> only on the single frame the key transitions from Down to Up.
    /// </remarks>
    public static bool IsKeyReleased(Keys key)
    {
        return !_keys.IsKeyDown(key) && _lastKeys.IsKeyDown(key);
    }

    /// <summary>
    /// Checks if the given gamepad button is currently held down.
    /// </summary>
    /// <param name="button">The gamepad button to check.</param>
    public static bool IsButtonDown(Buttons button)
    {
        return _buttons.IsButtonDown(button);
    }

    /// <summary>
    /// Checks if the given gamepad button was pressed this frame.
    /// </summary>
    public static bool IsButtonPressed(Buttons button)
    {
        return _buttons.IsButtonDown(button) && !_lastButtons.IsButtonDown(button);
    }

    /// <summary>
    /// Checks if the given gamepad button was released this frame.
    /// </summary>
    public static bool IsButtonReleased(Buttons button)
    {
        return !_buttons.IsButtonDown(button) && _lastButtons.IsButtonDown(button);
    }

    /// <summary>
    /// Registers a new named input action binding specific keys and buttons to a single name.
    /// </summary>
    /// <param name="name">The unique name for the action.</param>
    /// <param name="keys">The keyboard keys that trigger this action.</param>
    /// <param name="buttons">The gamepad buttons that trigger this action.</param>
    /// <exception cref="Exception">Thrown if an action with the same name already exists.</exception>
    public static void Add(string name, Keys[] keys, Buttons[] buttons)
    {
        if (_actions.ContainsKey(name))
            throw new Exception($"There already exists a InputAction with the name: '{name}'");
        _actions.Add(name, new InputAction(keys, buttons));
    }

    /// <summary>
    /// Checks if the named action is currently held down.
    /// </summary>
    /// <param name="name">The name of the action to check.</param>
    /// <exception cref="Exception">Thrown if the action name does not exist.</exception>
    public static bool IsDown(string name)
    {
        return !_actions.TryGetValue(name, out InputAction? action) 
            ? throw new Exception($"No InputAction with the name '{name}' exists") 
            : action.IsDown;
    }

    /// <summary>
    /// Checks if the named action was pressed this frame.
    /// </summary>
    /// <param name="name">The name of the action to check.</param>
    /// <exception cref="Exception">Thrown if the action name does not exist.</exception>
    public static bool IsPressed(string name)
    {
        return !_actions.TryGetValue(name, out InputAction? action) 
            ? throw new Exception($"No InputAction with the name '{name}' exists") 
            : action.IsPressed;
    }

    /// <summary>
    /// Checks if the named action was released this frame.
    /// </summary>
    /// <param name="name">The name of the action to check.</param>
    /// <exception cref="Exception">Thrown if the action name does not exist.</exception>
    public static bool IsReleased(string name)
    {
        return !_actions.TryGetValue(name, out InputAction? action) 
            ? throw new Exception($"No InputAction with the name '{name}' exists") 
            : action.IsReleased;
    }

    /// <summary>
    /// Retrieves the <see cref="InputAction"/> object associated with the given name.
    /// </summary>
    public static InputAction Action(string name)
    {
        return !_actions.TryGetValue(name, out InputAction? action) 
            ? throw new Exception($"No InputAction with the name '{name}' exists") 
            : action;
    }
}
