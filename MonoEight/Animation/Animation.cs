using System;
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

    public Action OnPlayed;
    public Action OnStopped;

    public Animation(Texture2D texture, Point size, float frameDuration = 1f, bool loops = true) : base(texture, size)
    {
        FrameDuration = frameDuration;
        Loop = loops;

        Reset();
    }

    public Animation(Texture2D texture, int size, float frameDuration = 1f, bool loops = true)
        : this(texture, new Point(size, size), frameDuration, loops) { }

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
        OnStopped?.Invoke();
    }

    public void Play()
    {
        Reset();

        _isPlaying = true;
        OnPlayed?.Invoke();
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