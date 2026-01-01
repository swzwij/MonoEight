namespace MonoEight;

/// <summary>
/// Represents an animation consisting of frames.
/// </summary>
/// <param name="name">The name of the animation.</param>
/// <param name="frames">The frames of the animation.</param>
public class Animation(string name, params int[] frames)
{
    /// <summary>
    /// Gets the name of the animation.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Gets the frames of the animation.
    /// </summary>
    public int[] Frames { get; } = frames;

    /// <summary>
    /// Gets or sets the duration of each frame in seconds.
    /// </summary>
    /// <remarks> Default is 0.05f (50 ms or 20 fps).</remarks>
    public float FrameDuration { get; set; } = 0.05f;

    /// <summary>
    /// Gets or sets a whether the animation should loop.
    /// </summary>
    /// <remarks> Default is true.</remarks>
    public bool Loop { get; set; } = true;
}
