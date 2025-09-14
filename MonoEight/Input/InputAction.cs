using System;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class InputAction
{
    private readonly Keys[] _keys;

    public bool IsPressed { get; private set; }
    public bool IsDown { get; private set; }
    public bool IsReleased { get; private set; }

    public InputAction(Keys[] keys)
    {
        _keys = keys;
    }

    public void Update(KeyboardState state, KeyboardState lastState)
    {
        Clear();

        int l = _keys.Length;
        for (int i = 0; i < l; i++)
        {
            Keys key = _keys[i];

            if (state.IsKeyDown(key))
                IsDown = true;

            if (state.IsKeyDown(key) && !lastState.IsKeyDown(key))
                IsPressed = true;

            if (!state.IsKeyDown(key) && lastState.IsKeyDown(key))
                IsReleased = true;
        }
    }

    private void Clear()
    {
        IsPressed = false;
        IsDown = false;
        IsReleased = false;
    }
}