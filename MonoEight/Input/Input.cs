using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public static class Input
{
    private static KeyboardState _keyboardState;
    private static KeyboardState _lastKeyboardState;

    private static GamePadState _gamePadState;
    private static GamePadState _lastGamePadState;

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

            axis += _gamePadState.ThumbSticks.Left.X > ControllerDeadZone ? 1 : 0;

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

            axis += _gamePadState.ThumbSticks.Left.Y > ControllerDeadZone ? 1 : 0;

            axis -= Convert.ToInt32(IsButtonDown(Buttons.DPadUp));
            axis += Convert.ToInt32(IsButtonDown(Buttons.DPadDown));

            return Math.Clamp(axis, -1, 1);
        }
    }

    public static Point InputAxis => new(HorizontalAxis, VerticalAxis);

    public static void Update()
    {
        _lastKeyboardState = _keyboardState;
        _keyboardState = Keyboard.GetState();

        _lastGamePadState = _gamePadState;
        _gamePadState = GamePad.GetState(PlayerIndex.One);

        UpdateActions();
    }

    public static bool IsKeyDown(Keys key)
    {
        return _keyboardState.IsKeyDown(key);
    }

    public static bool IsKeyPressed(Keys key)
    {
        return _keyboardState.IsKeyDown(key) && !_lastKeyboardState.IsKeyDown(key);
    }

    public static bool IsKeyReleased(Keys key)
    {
        return !_keyboardState.IsKeyDown(key) && _lastKeyboardState.IsKeyDown(key);
    }

    public static bool IsButtonDown(Buttons button)
    {
        return _gamePadState.IsButtonDown(button);
    }

    public static bool IsButtonPressed(Buttons button)
    {
        return _gamePadState.IsButtonDown(button) && !_lastGamePadState.IsButtonDown(button);
    }

    public static bool IsButtonReleased(Buttons button)
    {
        return !_gamePadState.IsButtonDown(button) && _lastGamePadState.IsButtonDown(button);
    }

    private static void UpdateActions()
    {
        foreach (InputAction action in _actions.Values)
            action.Update(_keyboardState, _lastKeyboardState);
    }

    public static void Add(string input, Keys[] keys, Buttons[] buttons)
    {
        if (_actions.ContainsKey(input))
            throw new Exception($"There already exists an input action with the name {input}");

        _actions.Add(input, new(keys, buttons));
    }

    public static InputAction Get(string trigger)
    {
        if (_actions.TryGetValue(trigger, out InputAction action))
            return action;
        else
            throw new Exception($"There is no input action called {trigger}");
    }
}