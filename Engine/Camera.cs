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
            _position.X = value.X - GameWindow.Width / 2;
            _position.Y = value.Y - GameWindow.Height / 2;

            _position.X = value.X == 0 ? _position.X * -1 : _position.X;
            _position.Y = value.Y == 0 ? _position.Y * -1 : _position.Y;

            Console.WriteLine("Camera Position");
            Console.WriteLine(GameWindow.Size);
            Console.WriteLine(value);
            Console.WriteLine(_position);

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