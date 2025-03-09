using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class SpriteSheet
{
    private Point _tileSize;

    private readonly Texture2D _texture;

    private Texture2D[] _sprites;

    public int SpriteCount => _sprites.Length;
    public Point TileSize => _tileSize;

    public Texture2D this[int index] => Get(index);

    public SpriteSheet(Texture2D texture, int tileSize)
    {
        _texture = texture;
        _tileSize = new Point(tileSize, tileSize);

        SpliceSprites();
    }

    public SpriteSheet(Texture2D texture, Point tileSize)
    {
        _texture = texture;
        _tileSize = tileSize;

        SpliceSprites();
    }

    public SpriteSheet(string texturePath, int tileSize)
    {
        _texture = ContentLoader.Load<Texture2D>(texturePath);
        _tileSize = new Point(tileSize, tileSize);

        SpliceSprites();
    }

    public SpriteSheet(string texturePath, Point tileSize)
    {
        _texture = ContentLoader.Load<Texture2D>(texturePath);
        _tileSize = tileSize;

        SpliceSprites();
    }

    private void SpliceSprites()
    {
        CalculateSpriteCount();

        int spriteIndex = 0;

        for (int y = 0; y < _texture.Height; y += _tileSize.Y)
        {
            for (int x = 0; x < _texture.Width; x += _tileSize.X)
            {
                _sprites[spriteIndex] = Splice(x, y);
                spriteIndex++;
            }
        }
    }

    private void CalculateSpriteCount()
    {
        int rows = _texture.Height / _tileSize.Y;
        int columns = _texture.Width / _tileSize.X;
        int spriteCount = rows * columns;

        _sprites = new Texture2D[spriteCount];
    }

    private Texture2D Splice(int x, int y)
    {
        Texture2D sprite = new(_texture.GraphicsDevice, _tileSize.X, _tileSize.Y);
        Color[] data = new Color[_tileSize.X * _tileSize.Y];
        _texture.GetData(0, new(x, y, _tileSize.X, _tileSize.Y), data, 0, data.Length);
        sprite.SetData(data);

        return sprite;
    }

    public Texture2D Get(int index)
    {
        if (index < 0 || index >= _sprites.Length)
            throw new IndexOutOfRangeException();

        return _sprites[index];
    }
}
