using Microsoft.Xna.Framework;

namespace MonoEight;

/// <summary>
/// Provides access to timing information for the current game frame.
/// </summary>
public static class Time
{
    private static GameTime _gameTime;

    public static float DeltaTime => (float)_gameTime.ElapsedGameTime.TotalSeconds;

    public static void Update(GameTime gameTime)
    {
        _gameTime = gameTime;
    }
}
