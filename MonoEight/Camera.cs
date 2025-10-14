using Microsoft.Xna.Framework;

namespace MonoEight;

public class Camera
{
    private Matrix _transform;
    private Vector2 _position;

    public Color BackgroundColor { get; set; }

    public Matrix Transform => _transform;
    public Vector2 Position
    {
        get => _position + new Vector2(MEWindow.Width / 2 , MEWindow.Height / 2);
        set
        {
            _position = new(value.X - MEWindow.Width / 2, value.Y - MEWindow.Height / 2);
            UpdatePosition();
        }
    }

    public Camera()
    {
        Position = Vector2.Zero;
        BackgroundColor = MEColors.Blue;
    }

    private void UpdatePosition()
    {
        _transform = Matrix.CreateTranslation((int)-_position.X, (int)-_position.Y, 0);
    }
}