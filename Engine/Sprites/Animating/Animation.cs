using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class Animation
{
    private readonly SpriteSheet _spriteSheet;
    private readonly float _frameDuration;
    private readonly bool _isLooping;

    private int _currentFrame;
    private float _frameTimer;
    private bool _isPlaying;

    public int CurrentFrame => _currentFrame;
    public bool IsPlaying => _isPlaying;
    public bool IsLooping => _isLooping;

    public Animation(SpriteSheet spriteSheet, float frameDuration, bool isLooping)
    {
        _spriteSheet = spriteSheet;
        _frameDuration = frameDuration;
        _isLooping = isLooping;
    }

    public void Play()
    {
        _isPlaying = true;
    }

    public void Stop()
    {
        _isPlaying = false;
    }

    public void Update(GameTime gameTime)
    {
        if (!_isPlaying)
            return;

        _frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_frameTimer < _frameDuration)
            return;

        _frameTimer = 0;
        _currentFrame++;

        if (_currentFrame < _spriteSheet.SpriteCount)
            return;

        if (_isLooping)
            _currentFrame = 0;
        else
            Stop();
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        Sprite.DrawCentered(spriteBatch, _spriteSheet[_currentFrame], position);
    }
}