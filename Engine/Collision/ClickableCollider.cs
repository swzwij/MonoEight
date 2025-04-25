
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
    private bool _isEnabled;

    public Vector2 Position => _position;
    public Vector2 Size => _size;

    public bool IsHovered => _isHovered;
    public bool IsClicked => _isClicked;

    public ClickableCollider(Vector2 size, Vector2 offset)
    {
        _size = size;
        _offset = offset;
        _isEnabled = true;
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

        if (colliderRect.Intersects(mouseRect))
        {
            _isHovered = true;

            if (Input.Mouse.LeftButtonPressed())
            {
                _isClicked = true;
                _isHovered = false;
            }
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
        Color drawColor = _isHovered ? Color.Yellow : _isClicked ? Color.Red : color;
        Debugger.DrawSquare(spriteBatch, _position, _size, drawColor);
    }
}