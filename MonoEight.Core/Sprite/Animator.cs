namespace MonoEight.Core.Sprite;

/// <summary>
/// A <see cref="Component"/> that renders animated sprites by cycling through frames form a <see cref="SpriteSheet"/>. 
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
    /// Gets or sets the default duration, in seconds, for each frame. 
    /// Used only for the default animation created in the constructor.
    /// </summary>
    public float FrameDuration { get; init; } = 0.1f;

    /// <summary>
    /// Gets or sets a value indicating whether the default animation should loop.
    /// Used only for the default animation created in the constructor.
    /// </summary>
    public bool Loop { get; init; } = true;

    /// <summary>
    /// Invoked when playback starts or resumes.
    /// </summary>
    public Action OnPlayed;

    /// <summary>
    /// Invoked when playback is stopped manually or when a non looping animation finishes.
    /// </summary>
    public Action OnStopped;

    /// <summary>
    /// Invoked when the current animation completes a cycle and loops back to the start.
    /// </summary>
    public Action OnLooped;

    /// <summary>
    /// Invoked when the current animation is switched to a new one. 
    /// Provides the name of the new animation.
    /// </summary>
    public Action<string> OnChanged;

    /// <summary>
    /// Invoked when a non-looping animation reaches its final frame.
    /// Provides the name of the finished animation.
    /// </summary>
    public Action<string> OnFinished;

    /// <summary>
    /// Initializes a new instance of the <see cref="Animator"/> class with a default animation using all frames in the sheet.
    /// </summary>
    /// <param name="spriteSheet">The source sprite sheet containing the frames.</param>
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
    /// Initializes a new instance of the <see cref="Animator"/> class with a given set of named animations.
    /// </summary>
    /// <param name="spriteSheet">The source sprite sheet containing the frames.</param>
    /// <param name="animations">The array of animations to register.</param>
    /// <exception cref="Exception">Thrown if the animations array is empty.</exception>
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

    protected override void Update()
    {
        if (!_isPlaying)
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
    /// Starts or resumes playback of the current animation.
    /// </summary>
    public void Play()
    {
        _isPlaying = true;
        OnPlayed?.Invoke();
    }

    /// <summary>
    /// Switches to the given animation and starts playback from the beginning.
    /// </summary>
    /// <remarks>
    /// If the specified animation is already playing, this method does nothing, unless <see cref="Stop"/> was called previously.
    /// </remarks>
    /// <param name="animationName">The name of the animation to play.</param>
    /// <exception cref="Exception">Thrown if the animation name is not found.</exception>
    public void Play(string animationName)
    {
        if (!_animations.TryGetValue(animationName, out Animation? newAnimation))
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
    /// Stops playback. The sprite remains on the current frame.
    /// </summary>
    public void Stop()
    {
        _isPlaying = false;
        OnStopped?.Invoke();
    }

    /// <summary>
    /// Pauses playback.
    /// </summary>
    public void Pause()
    {
        _isPlaying = false;
    }

    /// <summary>
    /// Resumes playback from the current frame.
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
