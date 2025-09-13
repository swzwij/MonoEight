using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public class Animation
{
    private readonly SpriteSheet _sheet;

    public int Index { get; set; } = 0;
    public float FrameDuration { get; set; } = 1f;
    public bool Loop { get; set; } = true;

    public SpriteSheet Sheet => _sheet;
    public int Count => _sheet.Count;
    public float Duration => Count * FrameDuration;

    public Animation(SpriteSheet sheet)
    {
        _sheet = sheet;
    }

    public void Reset()
    {
        Index = 0;
    }
}