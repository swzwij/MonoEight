using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class Animator
{
    private readonly SpriteSheet _spriteSheet;
    private int _currentFrame;
    private float _frameTimer;
    private float _frameDuration;
    private bool _isPlaying;
    private bool _isLooping;

    public int CurrentFrame => _currentFrame;
    public bool IsPlaying => _isPlaying;
    public bool IsLooping => _isLooping;

    public Animator(SpriteSheet spriteSheet, float frameDuration, bool isLooping)
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

        if (_frameTimer >= _frameDuration)
        {
            _frameTimer = 0;
            _currentFrame++;

            if (_currentFrame >= _spriteSheet.SpriteCount)
            {
                if (_isLooping)
                    _currentFrame = 0;
                else
                    Stop();
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        spriteBatch.Draw(_spriteSheet[_currentFrame], position, Color.White);
    }
}