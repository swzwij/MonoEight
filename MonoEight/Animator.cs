using System.Collections.Generic;

namespace MonoEight;

public class Animator
{
    private readonly Dictionary<string, Animation> _animations;
    private Animation _activeAnimation;
    private string _activeTrigger;

    public Animator(AnimationBinding[] animationBindings)
    {
        _animations = [];

        foreach (AnimationBinding binding in animationBindings)
            _animations.Add(binding.Trigger, binding.Animation);
    }

    public void Play(string trigger)
    {

    }
}