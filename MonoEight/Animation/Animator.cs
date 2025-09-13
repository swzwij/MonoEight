using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class Animator
{
    private readonly Dictionary<string, Animation> _animations;
    private Animation _activeAnimation;

    private float _timer;
    private bool _isPlaying;

    public Animation this[string trigger] => Get(trigger);

    public Animator(AnimationBinding[] animationBindings)
    {
        _animations = [];
        _timer = 0;

        foreach (AnimationBinding binding in animationBindings)
            _animations.Add(binding.Trigger, binding.Animation);
    }

    public Animation Get(string trigger)
    {
        if (!_animations.TryGetValue(trigger, out Animation animation))
            throw new IndexOutOfRangeException($"Trigger: '{trigger}' was not found in the animator");

        return animation;
    }

    public void Play(string trigger)
    {
        if (!_animations.TryGetValue(trigger, out Animation animation))
            throw new IndexOutOfRangeException($"Trigger: '{trigger}' was not found in the animator");

        _activeAnimation?.Reset();
        _activeAnimation = animation;
        _timer = 0;
        _isPlaying = true;
    }

    public void Stop()
    {
        _isPlaying = false;
        _timer = 0;
        _activeAnimation.Index--;
    }

    public void Update(GameTime gameTime)
    {
        if (!_isPlaying)
            return;

        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_timer < _activeAnimation.FrameDuration)
            return;

        _timer = 0;
        _activeAnimation.Index++;

        if (_activeAnimation.Index < _activeAnimation.Count)
            return;

        if (_activeAnimation.Loop)
            _activeAnimation.Index = 0;
        else
            Stop();
    }

    public void Draw(SpriteBatch spriteBatch, Point position)
    {
        _activeAnimation.Sheet.Draw(spriteBatch, _activeAnimation.Index, position);
    }
}