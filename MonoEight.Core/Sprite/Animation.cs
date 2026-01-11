namespace MonoEight.Core.Sprite;

/// <summary>
/// A named animation sequence defined by an array of frame indices.
/// </summary>
/// <param name="name">The unique name of the animation.</param>
/// <param name="frames">The indices of the frames in the <see cref="SpriteSheet"/> that make up the animation.</param>
public class Animation(string name, params int[] frames)
{
    /// <summary>
    /// Gets the name of the <see cref="Animation"/>.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Gets the array of indices.
    /// </summary>
    public int[] Frames { get; } = frames;

    /// <summary>
    /// Gets the duration of a single frame in seconds.
    /// </summary>
    /// <remarks>
    /// The default is 0.05 seconds, ~20 fps.
    /// </remarks>
    public float FrameDuration { get; init; } = 0.05f;

    /// <summary>
    /// Whether the <see cref="Animation"/> should loop continuously.
    /// </summary>
    /// <remarks>
    /// Default is <c>true</c>.
    /// </remarks>
    public bool Loop { get; init; } = true;
}
