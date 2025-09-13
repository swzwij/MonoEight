using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class Animation
{
    private readonly SpriteSheet _sheet;

    public int Index { get; set; } = 0;
    public float FrameDuration { get; set; } = 1f;
    public bool Looping { get; set; } = true;

    public Animation(SpriteSheet sheet)
    {
        _sheet = sheet;
    }

    public void Reset()
    {
        Index = 0;
    }
}