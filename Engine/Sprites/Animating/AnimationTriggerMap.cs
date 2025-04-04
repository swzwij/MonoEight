namespace MonoEight;

/// <summary>
/// Represents a mapping between an animation trigger and an animation.
/// This class is used to manage the relationship between triggers and animations in a sprite sheet.
/// </summary>
public class AnimationTriggerMap
{
    private readonly string _trigger;
    private readonly Animation _animation;

    public string Trigger => _trigger;
    public Animation Animation => _animation;

    public AnimationTriggerMap(string trigger, Animation animation)
    {
        _trigger = trigger;
        _animation = animation;
    }
}