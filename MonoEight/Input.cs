using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

/// <summary>
/// Provides static methods and properties for querying and managing keyboard, gamepad, and mouse input states, as well
/// as custom input actions. Enables unified access to input from multiple devices for use in games or interactive
/// applications.
/// </summary>
/// <remarks>
/// The Input class is designed to be updated once per frame by calling the Update method, which
/// refreshes the internal state of all supported input devices. It supports querying the current and previous states of
/// keys and buttons, detecting presses and releases, and managing custom input actions mapped to specific keys or
/// buttons.
/// </remarks>
public static class Input
{
    private static KeyboardState _keys;
    private static KeyboardState _lastKeys;
    private static GamePadState _buttons;
    private static GamePadState _lastButtons;
    private static readonly Dictionary<string, InputAction> _actions = [];

    private static MouseHandler _mouse = new();

    public static float ControllerDeadZone { get; set; } = 0.1f;
    public static int HorizontalAxis => GetHorizontalAxis();
    public static int VerticalAxis => GetVerticalAxis();
    public static Point InputAxis => new(HorizontalAxis, VerticalAxis);
    public static MouseHandler Mouse => _mouse;

    public static void Update()
    {
        _lastKeys = _keys;
        _keys = Keyboard.GetState();
        _lastButtons = _buttons;
        _buttons = GamePad.GetState(PlayerIndex.One);

        _mouse.Update();

        foreach (InputAction action in _actions.Values)
            action.Update(_keys, _lastKeys, _buttons, _lastButtons);
    }

    public static bool IsKeyDown(Keys key)
    {
        return _keys.IsKeyDown(key);
    }

    public static bool IsKeyPressed(Keys key)
    {
        return _keys.IsKeyDown(key) && !_lastKeys.IsKeyDown(key);
    }

    public static bool IsKeyReleased(Keys key)
    {
        return !_keys.IsKeyDown(key) && _lastKeys.IsKeyDown(key);
    }

    public static bool IsButtonDown(Buttons button)
    {
        return _buttons.IsButtonDown(button);
    }

    public static bool IsButtonPressed(Buttons button)
    {
        return _buttons.IsButtonDown(button) && !_lastButtons.IsButtonDown(button);
    }

    public static bool IsButtonReleased(Buttons button)
    {
        return !_buttons.IsButtonDown(button) && _lastButtons.IsButtonDown(button);
    }

    public static void Add(string name, Keys[] keys, Buttons[] buttons)
    {
        if (_actions.ContainsKey(name))
            throw new Exception($"There already exists a InputAction with the name: '{name}'");
        _actions.Add(name, new(keys, buttons));
    }

    public static bool IsDown(string name)
    {
        if (!_actions.TryGetValue(name, out InputAction action))
            throw new Exception($"No InputAction with the name '{name}' exists");
        return action.IsDown;
    }

    public static bool IsPressed(string name)
    {
        if (!_actions.TryGetValue(name, out InputAction action))
            throw new Exception($"No InputAction with the name '{name}' exists");
        return action.IsPressed;
    }

    public static bool IsReleased(string name)
    {
        if (!_actions.TryGetValue(name, out InputAction action))
            throw new Exception($"No InputAction with the name '{name}' exists");
        return action.IsReleased;
    }

    public static InputAction Action(string name)
    {
        if (!_actions.TryGetValue(name, out InputAction action))
            throw new Exception($"No InputAction with the name '{name}' exists");
        return action;
    }

    public static int GetHorizontalAxis()
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

    public static int GetVerticalAxis()
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