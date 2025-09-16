using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class Animation : SpriteSheet
{
    private int _index;
    private float _timer;
    private bool _isPlaying;

    public float FrameDuration { get; set; } = 1f;
    public bool Loop { get; set; } = true;

    public float Duration => Count * FrameDuration;

    public Animation(Texture2D texture, Point size) : base(texture, size)
    {
        Reset();
    }

    public Animation(Texture2D texture, int size) : this(texture, new Point(size, size)) { }

    public void Update(GameTime gameTime)
    {
        if (!_isPlaying)
            return;

        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer < FrameDuration)
            return;

        _timer = 0;
        _index++;

        if (_index < Count)
            return;

        if (Loop)
            _index = 0;
        else
        {
            _index--;
            Stop();
        }
    }

    public void Stop()
    {
        _isPlaying = false;
    }

    public void Play()
    {
        Reset();

        _isPlaying = true;
    }

    public new void Draw(SpriteBatch spriteBatch, Point position)
    {
        Draw(spriteBatch, _index, position);
    }

    private void Reset()
    {
        _index = 0;
        _timer = 0;
        _isPlaying = false;
    }
}