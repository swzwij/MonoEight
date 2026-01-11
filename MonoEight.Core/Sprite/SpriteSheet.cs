using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight.Core.Sprite;

/// <summary>
/// A collection of sprites sliced from a larger texture
/// </summary>
public class SpriteSheet
{
    private readonly Point _spriteSize;
    private readonly Texture2D _texture;
    private readonly Texture2D[] _sprites;
    private readonly int _rows;
    private readonly int _columns;

    /// <summary>
    /// Gets the total number of sprites in this sheet.
    /// </summary>
    public int Count => _rows * _columns;
    
    /// <summary>
    /// Gets the sprite <see cref="Texture2D"/> at the given index.
    /// </summary>
    /// <param name="index">The index of the sprite.</param>
    /// <returns>The sprite <see cref="Texture2D"/>.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown if the index is invalid.</exception>
    public Texture2D this[int index] => Get(index);

    /// <summary>
    /// Initializes a new instance of the <see cref="SpriteSheet"/> from the given <see cref="Texture2D"/>.
    /// </summary>
    /// <param name="texture">The <see cref="Texture2D"/> that will be sliced.</param>
    /// <param name="size">The size of a single sprite in the sheet.</param>
    public SpriteSheet(Texture2D texture, Point size)
    {
        _texture = texture;
        _spriteSize = size;

        _rows = _texture.Height / _spriteSize.Y;
        _columns = _texture.Width / _spriteSize.X;

        _sprites = new Texture2D[_rows * _columns];

        Splice();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="texture"><inheritdoc/></param>
    /// <param name="size">The size of a single sprite in the sheet.</param>
    public SpriteSheet(Texture2D texture, int size) : this(texture, new Point(size)) { }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="texture"><inheritdoc/></param>
    /// <param name="size">The size of a single sprite in the sheet.</param>
    /// <param name="indices">The indices to keep from the sliced sheet.</param>
    public SpriteSheet(Texture2D texture, int size, int[] indices) : this(texture, new Point(size))
    {
        Texture2D[] sprites = new Texture2D[indices.Length];

        for (int i = 0; i < indices.Length; i++)
            sprites[i] = _sprites[indices[i]];

        _sprites = sprites;
    }

    /// <summary>
    /// Gets the sprite <see cref="Texture2D"/> at the given index.
    /// </summary>
    /// <param name="index">The index of the sprite.</param>
    /// <returns>The sprite <see cref="Texture2D"/>.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown if the index is invalid.</exception>
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
