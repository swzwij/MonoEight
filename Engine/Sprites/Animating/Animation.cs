using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

/// <summary>
/// Represents an animation that can be played using a sprite sheet.
/// </summary>
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

    /// <summary>
    /// Plays the animation from the beginning.
    /// </summary>
    public void Play()
    {
        _currentFrame = 0;
        _frameTimer = 0;
        _isPlaying = true;
    }

    /// <summary>
    /// Stops the animation.
    /// </summary>
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
        SpriteRenderer.DrawCentered(spriteBatch, _spriteSheet[_currentFrame], position);
        new Sprite(spriteBatch, _spriteSheet[_currentFrame], position)
    }
}