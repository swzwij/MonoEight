using Microsoft.Xna.Framework;

namespace MonoEight.Core;

/// <summary>
/// Provides extension methods for converting between <see cref="Vector2"/> and <see cref="Point"/>.
/// </summary>
public static class VectorPointExtensions
{
    /// <summary>
    /// Converts a <see cref="Vector2"/> to a <see cref="Point"/> by casting the components to integers.
    /// </summary>
    /// <remarks>
    /// This operation truncates decimal values.
    /// </remarks>
    /// <param name="vector">The source vector.</param>
    /// <returns>A <see cref="Point"/> with integer coordinates.</returns>
    public static Point Int(this Vector2 vector)
    {
        return new Point((int)vector.X, (int)vector.Y);
    }
    
    /// <summary>
    /// Converts a <see cref="Point"/> to a <see cref="Vector2"/>.
    /// </summary>
    /// <param name="vector">The source point.</param>
    /// <returns>A <see cref="Vector2"/> with the same coordinates.</returns>
    public static Vector2 Float(this Point vector)
    {
        return new Vector2(vector.X, vector.Y);
    }
    
    /// <summary>
    /// Snaps a <see cref="Vector2"/> to the nearest integer coordinates while keeping it as a <see cref="Vector2"/>.
    /// </summary>
    /// <remarks>
    /// Effectively performs <c>new Vector2((int)x, (int)y)</c>.
    /// </remarks>
    /// <param name="vector">The source vector.</param>
    /// <returns>A <see cref="Vector2"/> with integer values.</returns>
    public static Vector2 Cast(this Vector2 vector)
    {
        return vector.Int().Float();
    }
}
