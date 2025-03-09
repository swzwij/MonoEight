using System.Numerics;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public static class Input
{
    private static KeyboardState _currentKeyboardState;
    private static KeyboardState _previousKeyboardState;

    public static void Update()
    {
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();
    }

    public static bool IsKeyDown(Keys key)
    {
        return _currentKeyboardState.IsKeyDown(key);
    }

    public static bool IsKeyPressed(Keys key)
    {
        return _currentKeyboardState.IsKeyDown(key) && !_previousKeyboardState.IsKeyDown(key);
    }

    public static bool IsKeyReleased(Keys key)
    {
        return !_currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyDown(key);
    }

    public static bool IsStartPressed => IsKeyPressed(Keys.Enter);

    public static bool IsBackPressed => IsKeyPressed(Keys.Escape);

    public static bool IsActionKeyPressed => IsKeyPressed(Keys.Z) || IsKeyPressed(Keys.K);

    public static bool IsActionKeyDown => IsKeyDown(Keys.Z) || IsKeyDown(Keys.K);

    public static bool IsActionKeyReleased => IsKeyReleased(Keys.Z) || IsKeyReleased(Keys.K);

    public static bool IsSecondaryActionKeyPressed => IsKeyPressed(Keys.X) || IsKeyPressed(Keys.L);

    public static bool IsSecondaryActionKeyDown => IsKeyDown(Keys.X) || IsKeyDown(Keys.L);

    public static bool IsSecondaryActionKeyReleased => IsKeyReleased(Keys.X) || IsKeyReleased(Keys.L);

    public static float HorizontalAxis
    {
        get
        {
            float axis = 0;

            if (IsKeyDown(Keys.Left) || IsKeyDown(Keys.A))
                axis -= 1;

            if (IsKeyDown(Keys.Right) || IsKeyDown(Keys.D))
                axis += 1;

            return axis;
        }
    }

    public static float VerticalAxis
    {
        get
        {
            float axis = 0;

            if (IsKeyDown(Keys.Up) || IsKeyDown(Keys.W))
                axis -= 1;

            if (IsKeyDown(Keys.Down) || IsKeyDown(Keys.S))
                axis += 1;

            return axis;
        }
    }

    public static Vector2 InputAxis => new Vector2(HorizontalAxis, VerticalAxis);
}