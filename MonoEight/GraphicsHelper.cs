using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

/// <summary>
/// Provides helper methods for graphics-related calculations.
/// </summary>
public static class GraphicsHelper
{
    /// <summary>
    /// Calculates the fullscreen rectangle based on the current graphics device and game window size.
    /// </summary>
    /// <param name="graphics">The graphics device to use for calculations.</param>
    /// <returns>A rectangle representing the fullscreen area.</returns>
    public static Rectangle CalculateFullscreenRect(GraphicsDevice graphics)
    {
        float aspectRatio = (float)MEWindow.Width / MEWindow.Height;

        int width = graphics.Viewport.Width;
        int height = (int)(width / aspectRatio);

        if (height > graphics.Viewport.Height)
        {
            height = graphics.Viewport.Height;
            width = (int)(height * aspectRatio);
        }

        int x = (graphics.Viewport.Width - width) / 2;
        int y = (graphics.Viewport.Height - height) / 2;

        return new Rectangle(x, y, width, height);
    }
}