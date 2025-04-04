using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

/// <summary>
/// Represents an animator that manages and plays animations based on triggers.
/// This class is used to control the animation state of a sprite using a sprite sheet.
/// </summary>
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

    /// <summary>
    /// Plays the animation associated with the specified trigger.
    /// If the trigger is the same as the current one, it does nothing.
    /// </summary>
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

    /// <summary>
    /// Stops the current animation.
    /// </summary>
    public void Stop()
    {
        _currentAnimation.Stop();
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