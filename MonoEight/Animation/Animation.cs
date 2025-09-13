using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class Animation
{
    private readonly SpriteSheet _sheet;

    private int _index;
    private float _timer;
    private bool _isPlaying;

    public float FrameDuration { get; set; } = 1f;
    public bool Loop { get; set; } = true;
    public float Scale { get; set; } = 1f;

    public SpriteSheet Sheet => _sheet;
    public int Count => _sheet.Count;
    public float Duration => Count * FrameDuration;

    public Animation(SpriteSheet sheet)
    {
        _sheet = sheet;
        _sheet.Renderer.Scale = Scale;
        Reset();
    }

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

    public void Draw(SpriteBatch spriteBatch, Point position)
    {
        _sheet.Draw(spriteBatch, _index, position);
    }

    private void Reset()
    {
        _index = 0;
        _timer = 0;
        _isPlaying = false;
    }
}