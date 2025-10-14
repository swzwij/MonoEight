namespace MonoEight;

public class Animation(string name, params int[] frames)
{
    public string Name { get; } = name;
    public int[] Frames { get; } = frames;
    public float FrameDuration { get; set; } = 0.05f;
    public bool Loop { get; set; } = true;
}