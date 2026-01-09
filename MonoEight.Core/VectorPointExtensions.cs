using Microsoft.Xna.Framework;

namespace MonoEight.Core;

/// <summary>
/// 
/// </summary>
public static class VectorPointExtensions
{
    /// <summary>
    /// Converts the specified Vector2 to a Point by truncating the X and Y components to their integer values.
    /// </summary>
    /// <remarks>
    /// The conversion truncates the fractional part of each component, it does not perform rounding.
    /// For example, a component value of 3.9 becomes 3.
    /// </remarks>
    /// <param name="vector">The Vector2 instance to convert to a Point.</param>
    /// <returns>A Point whose X and Y values are the integer parts of the corresponding components of the input vector.</returns>
    public static Point Int(this Vector2 vector)
    {
        return new Point((int)vector.X, (int)vector.Y);
    }

    /// <summary>
    /// Converts a Point structure to a Vector2 structure with the same X and Y values.
    /// </summary>
    /// <param name="vector">The Point to convert to a Vector2.</param>
    /// <returns>A Vector2 whose X and Y components are set to the X and Y values of the specified Point.</returns>
    public static Vector2 Float(this Point vector)
    {
        return new Vector2(vector.X, vector.Y);
    }

    /// <summary>
    /// Returns a new vector whose components are the integer parts of the specified vector, with any fractional parts
    /// removed.
    /// </summary>
    /// <remarks>
    /// This method truncates each component toward zero, effectively removing any fractional part.
    /// The resulting vector has floating-point components that represent the truncated integer values.
    /// </remarks>
    /// <param name="vector">The vector whose components will be truncated to their integer values.</param>
    /// <returns>A new Vector2 whose X and Y components are the integer parts of the corresponding components in 
    public static Vector2 Cast(this Vector2 vector)
    {
        return vector.Int().Float();
    }
}
