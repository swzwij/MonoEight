using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight.Core;

/// <summary>
/// Provides helper methods for calculating graphics related values.
/// </summary>
public static class GraphicsHelper
{
    /// <summary>
    /// Calculates the destination rectangle for rendering the game, maintaining the target aspect ratio.
    /// </summary>
    /// <remarks>
    /// This method fits the game's internal resolution (<see cref="MEWindow.Resolution"/>) into the current window size
    /// (<see cref="GraphicsDevice.Viewport"/>). The result is centered, creating bars if the window aspect ratio does
    /// not match the game's aspect ratio.
    /// </remarks>
    /// <param name="graphics">The graphics device containing the current viewport dimensions.</param>
    /// <returns>A <see cref="Rectangle"/> representing the area of the screen where the game should be drawn.</returns>
    public static Rectangle CalculateDisplayRect(GraphicsDevice graphics)
    {
        float aspectRatio = (float)MEWindow.Resolution.X / MEWindow.Resolution.Y;

        int viewportWidth = graphics.Viewport.Width;
        int viewportHeight = graphics.Viewport.Height;

        int width = viewportWidth;
        int height = (int)(width / aspectRatio);

        if (height > viewportHeight)
        {
            height = viewportHeight;
            width = (int)(height * aspectRatio);
        }

        int x = (viewportWidth - width) / 2;
        int y = (viewportHeight - height) / 2;

        return new Rectangle(x, y, width, height);
    }
}
