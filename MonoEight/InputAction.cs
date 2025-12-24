using System;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

/// <summary>
/// Represents a user input action that can be triggered by one or more keyboard keys or gamepad buttons.
/// </summary>
public class InputAction
{
    private readonly Keys[] _keys;
    private readonly Buttons[] _buttons;

    public bool IsPressed { get; private set; }
    public bool IsDown { get; private set; }
    public bool IsReleased { get; private set; }

    public Action OnPressed;
    public Action OnReleased;

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
            OnPressed?.Invoke();
        }

        if (!isDown && wasDown)
        {
            IsReleased = true;
            OnReleased?.Invoke();
        }
    }
}