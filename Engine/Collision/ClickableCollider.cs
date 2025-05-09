using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

/// <summary>
/// Represents a clickable collider for 2D collision detection and interaction.
/// </summary>
public class ClickableCollider
{
    private Vector2 _position;
    private Vector2 _size;
    private Vector2 _offset;

    private bool _isHovered;
    private bool _isClicked;
    private bool _isDown;
    private bool _isEnabled;

    public Vector2 Position => _position;
    public Vector2 Size => _size;

    public bool IsHovered => _isHovered;
    public bool IsClicked => _isClicked;
    public bool IsDown => _isDown;
    public bool IsEnabled
    {
        get => _isEnabled;
        set => _isEnabled = value;
    }

    public Action OnClick;

    public ClickableCollider(Vector2 position, Vector2 size, Vector2 offset = default)
    {
        _size = size;
        _offset = offset == default ? new Vector2(-size.X / 2, -size.Y / 2) : offset;
        _isEnabled = true;
        _position = position + _offset;
    }

    public ClickableCollider(Vector2 position, float width, float height, Vector2 offset = default)
        : this(position, new Vector2(width, height), offset)
    {
    }

    public ClickableCollider(Vector2 position, Point point, Vector2 offset = default)
        : this(position, point.ToVector2(), offset)
    {
    }

    public void Update(Vector2 position)
    {
        _position = position + _offset;
        _isHovered = false;
        _isClicked = false;

        if (!_isEnabled)
            return;

        Vector2 mousePosition = Input.Mouse.Position;
        Rectangle colliderRect = new((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y);
        Rectangle mouseRect = new((int)mousePosition.X, (int)mousePosition.Y, 1, 1);

        if (!colliderRect.Intersects(mouseRect))
            return;

        _isHovered = true;

        if (Input.Mouse.LeftButtonPressed())
        {
            _isClicked = true;
            _isDown = true;
        }
        else if (Input.Mouse.LeftButtonReleased())
        {
            _isClicked = false;
            _isDown = false;
            OnClick?.Invoke();
        }
    }

    /// <summary>
    /// Enables the clickable collider.
    /// </summary>
    public void Enable()
    {
        _isEnabled = true;
    }

    /// <summary>
    /// Disables the clickable collider.
    /// </summary>
    /// <remarks>When disabled, the collider will not respond to mouse events.</remarks>
    public void Disable()
    {
        _isEnabled = false;
    }

    public void Draw(SpriteBatch spriteBatch, Color color)
    {
        Color drawColor = _isClicked || _isDown ? Color.Red : _isHovered ? Color.Yellow : color;
        Debugger.DrawSquare(spriteBatch, _position, _size, drawColor);
    }
}