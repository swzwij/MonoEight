using Microsoft.Xna.Framework;

namespace MonoEight;

public class Camera
{
    private Matrix _transform;
    private Point _position;

    public Color BackgroundColor { get; set; }

    public Matrix Transform => _transform;
    public Point Position
    {
        get => _position + new Point(MEWindow.Width / 2 , MEWindow.Height / 2);
        set
        {
            _position = new(value.X - MEWindow.Width / 2, value.Y - MEWindow.Height / 2);
            UpdatePosition();
        }
    }

    public Camera()
    {
        Position = Point.Zero;
        BackgroundColor = Color.AliceBlue;
    }

    private void UpdatePosition()
    {
        _transform = Matrix.CreateTranslation(-_position.X, -_position.Y, 0);
    }
}