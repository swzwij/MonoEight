using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class SpriteSheet
{
    private readonly Point _spriteSize;
    private readonly Texture2D _texture;
    private readonly Texture2D[] _sprites;
    private readonly int _rows;
    private readonly int _columns;

    private int _lastDrawnIndex;

    public int Count => _rows * _columns;
    public Texture2D this[int index] => Get(index);
    public SpriteRenderer Renderer { get; set; }

    public SpriteSheet(Texture2D texture, Point size)
    {
        _texture = texture;
        _spriteSize = size;

        _rows = _texture.Height / _spriteSize.Y;
        _columns = _texture.Width / _spriteSize.X;

        _sprites = new Texture2D[_rows * _columns];

        Splice();

        Renderer = new(_sprites[0]);
    }

    public SpriteSheet(Texture2D texture, int size) : this(texture, new Point(size, size)) { }

    public Texture2D Get(int index)
    {
        ValidateIndex(index);
        return _sprites[index];
    }

    public void Draw(SpriteBatch spriteBatch, int index, Point position)
    {
        if (index != _lastDrawnIndex)
        {
            ValidateIndex(index);
            Renderer.Texture = _sprites[index];
        }

        Renderer.Draw(spriteBatch, position);
        _lastDrawnIndex = index;
    }

    private void Splice()
    {
        int index = 0;

        for (int y = 0; y < _texture.Height; y += _spriteSize.Y)
        {
            for (int x = 0; x < _texture.Width; x += _spriteSize.X)
            {
                _sprites[index] = Splice(x, y);
                index++;
            }
        }
    }

    private Texture2D Splice(int x, int y)
    {
        Texture2D sprite = new(_texture.GraphicsDevice, _spriteSize.X, _spriteSize.Y);
        Color[] data = new Color[_spriteSize.X * _spriteSize.Y];
        _texture.GetData(0, new(x, y, _spriteSize.X, _spriteSize.Y), data, 0, data.Length);
        sprite.SetData(data);
        return sprite;
    }

    private void ValidateIndex(int index)
    {
        if (index >= 0 && index < Count)
            return;

        throw new IndexOutOfRangeException($"{index} either negative or outside the range of the spritesheet.");
    }
}