using System;
using Microsoft.Xna.Framework;

namespace MonoEight;

public static class MERandom
{
    private static readonly Random _random = new();

    public static int Range(int min, int max)
    {
        return _random.Next(min, max);
    }

    public static float Range(float min, float max)
    {
        return (float)(_random.NextDouble() * (max - min) + min);
    }

    public static Vector2 Range(Vector2 min, Vector2 max)
    {
        return new Vector2(Range(min.X, max.X), Range(min.Y, max.Y));
    }
}