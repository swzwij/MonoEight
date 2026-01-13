using Microsoft.Xna.Framework.Input;

namespace MonoEight.Core.UserInput;

/// <summary>
/// Defines an action that can be triggered by specific keys or buttons.
/// </summary>
/// <remarks>
/// The state properties (<see cref="IsPressed"/>, <see cref="IsDown"/>, <see cref="IsReleased"/>) return <c>true</c> 
/// if any of the bound keys or buttons satisfy the condition.
/// </remarks>
public class InputAction
{
    private readonly Keys[] _keys;
    private readonly Buttons[] _buttons;

    /// <summary>
    /// Gets whether the action is pressed this frame.
    /// </summary>
    public bool IsPressed { get; private set; }
    
    /// <summary>
    /// Gets whether this action is currently down.
    /// </summary>
    public bool IsDown { get; private set; }
    
    /// <summary>
    /// Gets whether this action was released this frame.
    /// </summary>
    public bool IsReleased { get; private set; }

    /// <summary>
    /// Invoked when the action is pressed this frame.
    /// </summary>
    public Action OnPressed;
    
    /// <summary>
    /// Invoked when the action was released this frame.
    /// </summary>
    public Action OnReleased;

    /// <summary>
    /// Initializes a new instance of the <see cref="InputAction"/> class.
    /// </summary>
    /// <param name="keys">An array keyboard keys that trigger this action.</param>
    /// <param name="buttons">An array of gamepad buttons that trigger this action.</param>
    public InputAction(Keys[] keys, Buttons[] buttons)
    {
        _keys = keys;
        _buttons = buttons;
    }

    private void Clear()
    {
        IsPressed = false;
        IsDown = false;
        IsReleased = false;
    }

    /// <summary>
    /// Updates the internal state of the action based on the current and previous input states.
    /// </summary>
    /// <param name="keys">The current keyboard state.</param>
    /// <param name="lastKeys">The previous frame's keyboard state.</param>
    /// <param name="buttons">The current gamepad state.</param>
    /// <param name="lastButtons">The previous frame's gamepad state.</param>
    public void Update(KeyboardState keys, KeyboardState lastKeys, GamePadState buttons, GamePadState lastButtons)
    {
        Clear();
        UpdateKeyboard(keys, lastKeys);
        UpdateGamePad(buttons, lastButtons);
    }

    private void UpdateKeyboard(KeyboardState keys, KeyboardState lastKeys)
    {
        int length = _keys.Length;
        for (int i = 0; i < length; i++)
        {
            Keys key = _keys[i];
            UpdateState(keys.IsKeyDown(key), lastKeys.IsKeyDown(key));
        }
    }

    private void UpdateGamePad(GamePadState buttons, GamePadState lastButtons)
    {
        int length = _buttons.Length;
        for (int i = 0; i < length; i++)
        {
            Buttons button = _buttons[i];
            UpdateState(buttons.IsButtonDown(button), lastButtons.IsButtonDown(button));
        }
    }

    private void UpdateState(bool isDown, bool wasDown)
    {
        if (isDown)
            IsDown = true;

        if (isDown && !wasDown)
        {
            IsPressed = true;
            
            if (!IsPressed)
                OnPressed?.Invoke();
        }

        if (!isDown && wasDown)
        {
            IsReleased = true;
            OnReleased?.Invoke();
        }
    }
}
