using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public static class Input
{
    private static KeyboardState _keys;
    private static KeyboardState _lastKeys;

    private static GamePadState _buttons;
    private static GamePadState _lastButtons;

    public static float ControllerDeadZone { get; set; } = 0.1f;

    private static readonly Dictionary<string, InputAction> _actions = [];

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

    public static Point InputAxis => new(HorizontalAxis, VerticalAxis);

    public static void Update()
    {
        _lastKeys = _keys;
        _keys = Keyboard.GetState();

        _lastButtons = _buttons;
        _buttons = GamePad.GetState(PlayerIndex.One);

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

    public static void Add(string input, Keys[] keys, Buttons[] buttons)
    {
        if (_actions.ContainsKey(input))
            throw new Exception($"There already exists an input action with the name '{input}'");

        _actions.Add(input, new(keys, buttons));
    }

    public static bool IsPressed(string trigger)
    {
        return _actions[trigger].IsPressed;
    }

    public static bool IsDown(string trigger)
    {
        return _actions[trigger].IsDown;
    }

    public static bool IsReleased(string trigger)
    {
        return _actions[trigger].IsReleased;
    }
}