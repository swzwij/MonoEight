using System;
using System.Collections.Generic;

namespace MonoEight;

/// <summary>
/// Animates a sprite using a sprite sheet and defined animations.
/// </summary>
public class Animator : SpriteRenderer
{
    private readonly SpriteSheet _spriteSheet;
    private readonly Dictionary<string, Animation> _animations;

    private Animation _currentAnimation;
    private int _frameIndex;
    private float _timer;
    private bool _isPlaying;

    /// <summary>
    /// Gets or sets the default duration of each frame in seconds.
    /// </summary>
    /// <remarks>
    /// Default is 0.1f (100 ms or 10 fps).
    /// Overridden by individual animation's FrameDuration if set.
    /// </remarks>
    public float FrameDuration { get; set; } = 0.1f;

    /// <summary>
    /// Gets or sets whether the default animation should loop.
    /// </summary>
    /// <remarks>
    /// Default is true.
    /// Overridden by individual animation's Loop if set.
    /// </remarks>
    public bool Loop { get; set; } = true;

    /// <summary>
    /// Event invoked when the animation is played.
    /// </summary>
    public Action OnPlayed;

    /// <summary>
    /// Event invoked when the animation is stopped.
    /// </summary>
    public Action OnStopped;

    /// <summary>
    /// Event invoked when the animation loops.
    /// </summary>
    public Action OnLooped;

    /// <summary>
    /// Event invoked when the animation changes.
    /// </summary>
    public Action<string> OnChanged;

    /// <summary>
    /// Event invoked when the animation finishes playing.
    /// </summary>
    public Action<string> OnFinished;

    /// <summary>
    /// Initializes a new instance of the <see cref="Animator"/> class with a sprite sheet.
    /// </summary>
    /// <param name="spriteSheet">The sprite sheet to use for animations.</param>
    public Animator(SpriteSheet spriteSheet)
        : base(spriteSheet.Count > 0 ? spriteSheet[0] : null)
    {
        _spriteSheet = spriteSheet;
        _animations = [];

        int[] allFrames = new int[spriteSheet.Count];
        for (int i = 0; i < spriteSheet.Count; i++)
            allFrames[i] = i;

        _currentAnimation = new Animation("_default", allFrames)
        {
            FrameDuration = FrameDuration,
            Loop = Loop
        };

        Reset();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Animator"/> class with a sprite sheet and animations.
    /// </summary>
    /// <param name="spriteSheet">The sprite sheet to use for animations.</param>
    /// <param name="animations">The animations to define.</param>
    public Animator(SpriteSheet spriteSheet, Animation[] animations)
        : base(spriteSheet.Count > 0 ? spriteSheet[0] : null)
    {
        _spriteSheet = spriteSheet;
        _animations = [];

        foreach (Animation animation in animations)
            _animations[animation.Name] = animation;

        if (animations.Length == 0)
            throw new Exception("Animator requires at least one animation!");

        _currentAnimation = animations[0];
        Reset();
    }

    /// <summary>
    /// Handles the update logic for the animator.
    /// </summary>
    protected override void Update()
    {
        if (!_isPlaying || _currentAnimation == null)
            return;

        _timer += Time.DeltaTime;

        float duration = _currentAnimation.Name == "_default"
            ? FrameDuration
            : _currentAnimation.FrameDuration;

        if (_timer < duration)
            return;

        _timer = 0;
        _frameIndex++;

        if (_frameIndex < _currentAnimation.Frames.Length)
        {
            UpdateTexture();
            return;
        }

        bool shouldLoop = _currentAnimation.Name == "_default"
            ? Loop
            : _currentAnimation.Loop;

        if (shouldLoop)
        {
            _frameIndex = 0;
            UpdateTexture();
            OnLooped?.Invoke();
        }
        else
        {
            _frameIndex = _currentAnimation.Frames.Length - 1;
            Stop();
            OnFinished?.Invoke(_currentAnimation.Name);
        }
    }

    /// <summary>
    /// Plays the current animation.
    /// </summary>
    public void Play()
    {
        _isPlaying = true;
        OnPlayed?.Invoke();
    }

    /// <summary>
    /// Plays the specified animation by name.
    /// </summary>
    /// <param name="animationName">The name of the animation to play.</param>
    public void Play(string animationName)
    {
        if (!_animations.TryGetValue(animationName, out Animation newAnimation))
            throw new Exception($"Animation '{animationName}' not found!");

        if (_currentAnimation != newAnimation)
        {
            _currentAnimation = newAnimation;
            _frameIndex = 0;
            _timer = 0;
            UpdateTexture();
            OnChanged?.Invoke(animationName);
        }

        _isPlaying = true;
        OnPlayed?.Invoke();
    }

    /// <summary>
    /// Stops the current animation.
    /// </summary>
    public void Stop()
    {
        _isPlaying = false;
        OnStopped?.Invoke();
    }

    /// <summary>
    /// Pauses the current animation.
    /// </summary>
    public void Pause()
    {
        _isPlaying = false;
    }

    /// <summary>
    /// Resumes the current animation.
    /// </summary>
    public void Resume()
    {
        _isPlaying = true;
    }

    private void Reset()
    {
        _frameIndex = 0;
        _timer = 0;
        _isPlaying = false;

        if (_currentAnimation.Frames.Length == 0)
            return;

        Texture = _spriteSheet[_currentAnimation.Frames[0]];
    }

    private void UpdateTexture()
    {
        if (_frameIndex >= _currentAnimation.Frames.Length)
            return;

        Texture = _spriteSheet[_currentAnimation.Frames[_frameIndex]];
    }
}
