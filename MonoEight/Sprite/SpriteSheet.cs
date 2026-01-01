using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

/// <summary>
/// Represents a collection of equally sized sprites extracted from a single texture.
/// </summary>
public class SpriteSheet
{
    private readonly Point _spriteSize;
    private readonly Texture2D _texture;
    private readonly Texture2D[] _sprites;
    private readonly int _rows;
    private readonly int _columns;

    public int Count => _rows * _columns;
    public Texture2D this[int index] => Get(index);

    public SpriteSheet(Texture2D texture, Point size)
    {
        _texture = texture;
        _spriteSize = size;

        _rows = _texture.Height / _spriteSize.Y;
        _columns = _texture.Width / _spriteSize.X;

        _sprites = new Texture2D[_rows * _columns];

        Splice();
    }

    public SpriteSheet(Texture2D texture, int size) : this(texture, new Point(size)) { }

    public SpriteSheet(Texture2D texture, int size, int[] indices) : this(texture, new Point(size))
    {
        Texture2D[] sprites = new Texture2D[indices.Length];

        for (int i = 0; i < indices.Length; i++)
            sprites[i] = _sprites[indices[i]];

        _sprites = sprites;
    }

    public Texture2D Get(int index)
    {
        ValidateIndex(index);
        return _sprites[index];
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

        string message = $"{index} is either negative or outside the range of the spritesheet: {Count}.";
        throw new IndexOutOfRangeException(message);
    }
}
