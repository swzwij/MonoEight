using Microsoft.Xna.Framework.Audio;

namespace MonoEight.Core.Audio;

public class AudioMap
{
    public string Name { get; init; }
    public SoundEffect SoundEffect { get; init; }

    public AudioMap(string name, SoundEffect soundEffect)
    {
        Name = name;
        SoundEffect = soundEffect;
    }

    public AudioMap(string name, string path)
    {
        Name = name;
        SoundEffect = Content.Load<SoundEffect>(path);
    }
}
