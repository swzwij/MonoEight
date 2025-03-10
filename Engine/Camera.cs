using System;
using Microsoft.Xna.Framework;

namespace MonoEight;

public class Camera
{
    private static Matrix _transform;
    private static Vector2 _position;

    public static Matrix Transform => _transform;
    public static Vector2 Position
    {
        get => _position;
        set
        {
            _position = value;
            UpdatePosition();
        }
    }
    public static Vector2 RelativePosition
    {
        get => new(_position.X + GameWindow.Width / 2, _position.Y + GameWindow.Height / 2);
        set
        {
            Console.WriteLine(value);

            _position = new(value.X - GameWindow.Width / 2, value.Y - GameWindow.Height / 2);
            UpdatePosition();
        }
    }

    public static void Initialize()
    {
        Position = Vector2.Zero;
        UpdatePosition();
    }

    private static void UpdatePosition()
    {
        _transform = Matrix.CreateTranslation(-_position.X, -_position.Y, 0);
    }
}