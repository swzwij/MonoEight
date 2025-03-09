using Microsoft.Xna.Framework;

namespace MonoEight;

public class Transform
{
    private Vector2 _position;
    private float _rotation;
    private float _scale;

    public Vector2 Position
    {
        get => _position;
        set => _position = value;
    }

    public float Rotation
    {
        get => _rotation;
        set => _rotation = value;
    }

    public float Scale
    {
        get => _scale;
        set => _scale = value;
    }

    public Transform(Vector2 position, float rotation, float scale)
    {
        _position = position;
        _rotation = rotation;
        _scale = scale;
    }

    public Transform(Vector2 position, float rotation)
    {
        _position = position;
        _rotation = rotation;
        _scale = 1;
    }

    public Transform(Vector2 position)
    {
        _position = position;
        _rotation = 0;
        _scale = 1;
    }

    public Transform()
    {
        _position = Vector2.Zero;
        _rotation = 0;
        _scale = 1;
    }


}