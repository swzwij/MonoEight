using System;

namespace MonoEight;

public class Animator : SpriteRenderer
{
    private readonly SpriteSheet _spriteSheet;

    private int _index;
    private int _lastDrawnIndex;
    private float _timer;
    private bool _isPlaying;

    public float FrameDuration { get; set; } = 1f;
    public bool Loop { get; set; } = true;

    public Action OnPlayed;
    public Action OnStopped;
    public Action OnLooped;

    public Animator(GameObject gameObject, SpriteSheet spriteSheet) : base(gameObject)
    {
        _spriteSheet = spriteSheet;
        Reset();
    }

    private void Update()
    {
        if (!_isPlaying)
            return;

        _timer += Time.DeltaTime;

        if (_timer < FrameDuration)
            return;

        _timer = 0;
        _index++;

        if (_index < _spriteSheet.Count)
        {
            TrySetTexture();
            return;
        }

        if (Loop)
        {
            _index = 0;
            OnLooped?.Invoke();
        }
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
        _isPlaying = true;
        OnPlayed?.Invoke();
    }

    private void Reset()
    {
        _index = 0;
        _lastDrawnIndex = -1;
        _timer = 0;
        _isPlaying = false;

        Texture = _spriteSheet[0];
    }

    private void TrySetTexture()
    {
        if (_index == _lastDrawnIndex)
            return;

        Texture = _spriteSheet[_index];
        _lastDrawnIndex = _index;
    }
}