namespace MonoEight;

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