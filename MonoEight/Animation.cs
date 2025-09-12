using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class Animation
{
    private readonly SpriteSheet _sheet;

    private int _index;
    private float _frameTimer;

    public float FrameDuration { get; set; } = 1f;
    public bool Looping { get; set; } = true;

    public Animation(SpriteSheet sheet)
    {
        _sheet = sheet;
        _index = 0;
    }

    public void Update(GameTime gameTime)
    {
        _frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_frameTimer < FrameDuration)
            return;

        _frameTimer = 0;
        _index++;

        if (_index < _sheet.Count)
            return;

        _index = 0;
    }

    public void Draw(SpriteBatch spriteBatch, Point position)
    {
        _sheet.Draw(spriteBatch, _index, position);
    }
}