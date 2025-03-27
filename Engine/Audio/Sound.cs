using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MonoEight;

public static class Sound
{
    public static void Play(SoundEffect sound, float volume = 1f, float pitch = 0f, float pan = 0f)
    {
        SoundEffectInstance instance = sound.CreateInstance();
        instance.Volume = volume;
        instance.Pitch = pitch;
        instance.Pan = pan;
        instance.Play();
    }

    public static void Play(Song sound, bool repeat = false)
    {
        MediaPlayer.IsRepeating = repeat;
        MediaPlayer.Play(sound);
    }
}