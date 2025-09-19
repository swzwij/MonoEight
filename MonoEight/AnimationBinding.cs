namespace MonoEight;

public class AnimationBinding
{
    public string Trigger { get; private set; }
    public Animation Animation { get; private set; }

    public AnimationBinding(string trigger, Animation animation)
    {
        Trigger = trigger;
        Animation = animation;
    }
}