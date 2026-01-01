using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight.Core;

/// <summary>
/// Provides helper methods for calculating graphics related values such as display rectangles.
/// </summary>
public static class GraphicsHelper
{
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
