using Microsoft.Xna.Framework;

namespace MonoEight;

public static class Time
{
    private static GameTime _gameTime;

    public static float DeltaTime => (float)_gameTime.ElapsedGameTime.TotalSeconds;

    public static void Update(GameTime gameTime)
    {
        _gameTime = gameTime;
    }
}