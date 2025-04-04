using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

/// <summary>
/// Handles input from keyboard, gamepad, and mouse for the game.
/// Provides methods to check the state of keys, buttons, and triggers.
/// </summary>
public static class Input
{
    private static KeyboardState _currentKeyboardState;
    private static KeyboardState _previousKeyboardState;
    private static GamePadState _currentGamePadState;
    private static GamePadState _previousGamePadState;
    private static readonly PlayerIndex _playerIndex = PlayerIndex.One;
    private static readonly MouseHandler _mouseHandler = new();

    public static MouseHandler Mouse => _mouseHandler;

    public static void Update()
    {
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();

        _previousGamePadState = _currentGamePadState;
        _currentGamePadState = GamePad.GetState(_playerIndex);

        _mouseHandler.Update();
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

    public static bool IsGamePadConnected()
    {
        return _currentGamePadState.IsConnected;
    }

    public static bool IsButtonDown(Buttons button)
    {
        return _currentGamePadState.IsButtonDown(button);
    }

    public static bool IsButtonPressed(Buttons button)
    {
        return _currentGamePadState.IsButtonDown(button) && !_previousGamePadState.IsButtonDown(button);
    }

    public static bool IsButtonReleased(Buttons button)
    {
        return !_currentGamePadState.IsButtonDown(button) && _previousGamePadState.IsButtonDown(button);
    }

    public static float GetLeftTrigger()
    {
        return _currentGamePadState.Triggers.Left;
    }

    public static float GetRightTrigger()
    {
        return _currentGamePadState.Triggers.Right;
    }

    public static Vector2 GetLeftStick()
    {
        return _currentGamePadState.ThumbSticks.Left;
    }

    public static Vector2 GetRightStick()
    {
        return _currentGamePadState.ThumbSticks.Right;
    }

    public static bool IsStartPressed => IsKeyPressed(Keys.Enter) || IsButtonPressed(Buttons.Start);

    public static bool IsBackPressed => IsKeyPressed(Keys.Escape) || IsButtonPressed(Buttons.Back);

    public static bool IsActionKeyPressed => IsKeyPressed(Keys.Z) || IsKeyPressed(Keys.K) || IsButtonPressed(Buttons.A);

    public static bool IsActionKeyDown => IsKeyDown(Keys.Z) || IsKeyDown(Keys.K) || IsButtonDown(Buttons.A);

    public static bool IsActionKeyReleased => IsKeyReleased(Keys.Z) || IsKeyReleased(Keys.K) || IsButtonReleased(Buttons.A);

    public static bool IsSecondaryActionKeyPressed => IsKeyPressed(Keys.X) || IsKeyPressed(Keys.L) || IsButtonPressed(Buttons.B) || IsButtonPressed(Buttons.X);

    public static bool IsSecondaryActionKeyDown => IsKeyDown(Keys.X) || IsKeyDown(Keys.L) || IsButtonDown(Buttons.B) || IsButtonDown(Buttons.X);

    public static bool IsSecondaryActionKeyReleased => IsKeyReleased(Keys.X) || IsKeyReleased(Keys.L) || IsButtonReleased(Buttons.B) || IsButtonReleased(Buttons.X);

    public static float HorizontalAxis
    {
        get
        {
            float axis = 0;

            if (IsKeyDown(Keys.Left) || IsKeyDown(Keys.A))
                axis -= 1;

            if (IsKeyDown(Keys.Right) || IsKeyDown(Keys.D))
                axis += 1;

            if (IsGamePadConnected())
            {
                axis += GetLeftStick().X;

                if (IsButtonDown(Buttons.DPadLeft))
                    axis -= 1;

                if (IsButtonDown(Buttons.DPadRight))
                    axis += 1;
            }

            return MathHelper.Clamp(axis, -1, 1);
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

            if (IsGamePadConnected())
            {
                axis -= GetLeftStick().Y;

                if (IsButtonDown(Buttons.DPadUp))
                    axis -= 1;

                if (IsButtonDown(Buttons.DPadDown))
                    axis += 1;
            }

            return MathHelper.Clamp(axis, -1, 1);
        }
    }

    public static Vector2 InputAxis => new(HorizontalAxis, VerticalAxis);

    public static bool IsUpPressed => IsKeyPressed(Keys.Up) || IsKeyPressed(Keys.W) ||
                                     (IsGamePadConnected() && (IsButtonPressed(Buttons.DPadUp) || GetLeftStick().Y > 0.5f));

    public static bool IsDownPressed => IsKeyPressed(Keys.Down) || IsKeyPressed(Keys.S) ||
                                       (IsGamePadConnected() && (IsButtonPressed(Buttons.DPadDown) || GetLeftStick().Y < -0.5f));

    public static bool IsLeftPressed => IsKeyPressed(Keys.Left) || IsKeyPressed(Keys.A) ||
                                       (IsGamePadConnected() && (IsButtonPressed(Buttons.DPadLeft) || GetLeftStick().X < -0.5f));

    public static bool IsRightPressed => IsKeyPressed(Keys.Right) || IsKeyPressed(Keys.D) ||
                                        (IsGamePadConnected() && (IsButtonPressed(Buttons.DPadRight) || GetLeftStick().X > 0.5f));
}