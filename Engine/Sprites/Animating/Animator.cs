using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class Animator
{
    private readonly Dictionary<string, Animation> _animations = [];
    private Animation _currentAnimation;
    private string _currentTrigger = string.Empty;

    public Animator(AnimationTriggerMap[] animationTriggerMaps, int startIndex = 0)
    {
        foreach (AnimationTriggerMap animationTriggerMap in animationTriggerMaps)
            _animations.Add(animationTriggerMap.Trigger, animationTriggerMap.Animation);

        if (startIndex >= 0 && startIndex < animationTriggerMaps.Length)
        {
            _currentAnimation = animationTriggerMaps[startIndex].Animation;
            _currentTrigger = animationTriggerMaps[startIndex].Trigger;
        }

        _currentAnimation.Play();
    }

    public void Play(string trigger)
    {
        if (trigger == _currentTrigger)
            return;

        if (!_animations.TryGetValue(trigger, out Animation animation))
            return;

        _currentAnimation.Stop();
        _currentAnimation = animation;
        _currentTrigger = trigger;
        _currentAnimation.Play();
    }

    public void Update(GameTime gameTime)
    {
        _currentAnimation.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        _currentAnimation.Draw(spriteBatch, position);
    }
}