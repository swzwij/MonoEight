using System;
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
}