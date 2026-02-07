using Microsoft.Xna.Framework;

namespace MonoEight.Core;

/// <summary>
/// Provides access to the global game time variables from the <see cref="GameTime"/> class.
/// </summary>
public static class Time
{
    /// <summary>
    /// The MonoGame <see cref="GameTime"/> class.
    /// </summary>
    public static GameTime GameTime { get; private set; } = null!;

    /// <summary>
    /// The time in seconds it took to complete the last frame.
    /// </summary>
    public static float DeltaTime => (float)GameTime.ElapsedGameTime.TotalSeconds;
    
    /// <summary>
    /// Updated the <see cref="GameTime"/> in this class.
    /// </summary>
    /// <param name="gameTime"><see cref="GameTime"/></param>
    public static void Update(GameTime gameTime)
    {
        GameTime = gameTime;
    }
}
