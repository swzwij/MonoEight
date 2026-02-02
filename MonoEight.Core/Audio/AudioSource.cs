using Microsoft.Xna.Framework.Audio;

namespace MonoEight.Core.Audio;

public class AudioSource : Component
{
    private readonly Dictionary<string, SoundEffect> _soundEffects = [];

    public AudioSource(AudioMap[] audioMaps)
    {
        foreach (AudioMap audioMap in audioMaps)
            _soundEffects.Add(audioMap.Name, audioMap.SoundEffect);
    }

    public void Play(string name)
    {
        if (!_soundEffects.TryGetValue(name, out SoundEffect? soundEffect))
            throw  new KeyNotFoundException($"Sound effect {name} not found");
        
        soundEffect.Play();
    }
}
