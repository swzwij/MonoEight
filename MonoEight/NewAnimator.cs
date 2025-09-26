using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class NewAnimation
{
    private readonly int[] _indices;

    public int[] Indices { get; }
    public float FrameDuraction { get; set; } = 1;
    public bool Loop { get; set; } = false;

    public int Count => _indices.Length;
    public float Duration => Count * FrameDuraction;

    public int this[int index] => _indices[index];

    public NewAnimation(params int[] indices)
    {
        _indices = indices;
    }
}

public class NewAnimationMap
{
    public string Name { get; }
    public NewAnimation Animation { get; }

    public NewAnimationMap(string name, params int[] indices)
    {
        Name = name;
        Animation = new(indices);
    }
}

public class NewAnimator : SpriteRenderer
{
    private readonly Dictionary<string, NewAnimation> _animations;
    private readonly SpriteSheet _spriteSheet;

    private NewAnimation _animation;
    private int _index;
    private float _timer;
    private bool _isPlaying;

    public NewAnimation this[string name] => Get(name);

    public NewAnimator(GameObject gameObject, SpriteSheet spriteSheet, NewAnimationMap[] animations) : base(gameObject)
    {
        _animations = [];
        _spriteSheet = spriteSheet;
        _index = 0;
        _timer = 0;

        int l = animations.Length;
        for (int i = 0; i < l; i++)
            _animations.TryAdd(animations[i].Name, animations[i].Animation);
    }

    private void Update()
    {
        if (!_isPlaying)
            return;

        _timer += Time.DeltaTime;

        if (_timer < _animation.FrameDuraction)
            return;

        _timer = 0;
        _index++;

        if (_index < _animation.Count)
            return;

        Texture = _spriteSheet[_animation[_index]];

        if (_animation.Loop)
        {
            _index = 0;
            return;
        }

        _isPlaying = false;
    }

    public void Play(string name)
    {
        _animation = Get(name);
        _isPlaying = true;
        _index = 0;
        _timer = 0;
    }

    public void Stop()
    {
        _isPlaying = false;
    }

    public NewAnimation Get(string name)
    {
        if (!_animations.TryGetValue(name, out NewAnimation animation))
            throw new IndexOutOfRangeException($"Animator doesn't have a animaiton called: '{name}'");

        return animation;
    }
}