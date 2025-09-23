using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class NewAnimation
{
    private readonly int[] _indices;

    public int[] Indices { get; }
    public float FrameDuraction { get; set; }
    public bool Loop { get; set; }

    public int Count => _indices.Length;
    public float Duration => Count * FrameDuraction;

    public NewAnimation(params int[] indices)
    {
        _indices = indices;
    }
}

public class NewAnimator : SpriteRenderer
{
    private readonly Dictionary<string, NewAnimation> _animations;

    private NewAnimation _animation;
    private int _index;
    private float _timer;
    private bool _isPlaying;

    public NewAnimation this[string name] => Get(name);

    public NewAnimator(GameObject gameObject) : base(gameObject)
    {

    }

    private void Update()
    {
        if (_isPlaying)
            return;

        _timer += Time.DeltaTime;

        if (_timer < _animation.FrameDuraction)
            return;

        _timer = 0;
        _index++;

        if (_index < _animation.Count)
            return;

        if (_animation.Loop)
        {
            _index = 0;
            return;
        }

        _index--;
        _isPlaying = false;
    }

    private void Draw(SpriteBatch spriteBatch)
    {

    }

    public void Play(string name)
    {
        _animation = Get(name);
    }

    public NewAnimation Get(string name)
    {
        if (!_animations.TryGetValue(name, out NewAnimation animation))
            throw new IndexOutOfRangeException($"Animator doesn't have a animaiton called: '{name}'");

        return animation;
    }
}