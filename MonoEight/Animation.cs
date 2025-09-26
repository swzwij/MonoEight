namespace MonoEight;

public class Animation
{
    public string Name { get; }
    public int[] Frames { get; }
    public float FrameDuration { get; set; } = 0.05f;
    public bool Loop { get; set; } = true;

    public Animation(string name, params int[] frames)
    {
        Name = name;
        Frames = frames;
    }
}