using System;
using System.Collections.Generic;

namespace MonoEight;

public class Animator : SpriteRenderer
{
    private readonly SpriteSheet _spriteSheet;
    private readonly Dictionary<string, Animation> _animations;

    private Animation _currentAnimation;
    private int _frameIndex;
    private float _timer;
    private bool _isPlaying;

    public float FrameDuration { get; set; } = 0.1f;
    public bool Loop { get; set; } = true;

    public Action OnPlayed;
    public Action OnStopped;
    public Action OnLooped;
    public Action<string> OnChanged;
    public Action<string> OnFinished;

    public Animator(GameObject gameObject, SpriteSheet spriteSheet) : base(gameObject)
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

    public Animator(GameObject gameObject, SpriteSheet spriteSheet, Animation[] animations) : base(gameObject)
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

    private void Update()
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

    public void Play()
    {
        _isPlaying = true;
        OnPlayed?.Invoke();
    }

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

    public void Stop()
    {
        _isPlaying = false;
        OnStopped?.Invoke();
    }

    public void Pause()
    {
        _isPlaying = false;
    }

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