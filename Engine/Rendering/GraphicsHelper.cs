using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEight;

public static class GraphicsHelper
{
    public static Rectangle CalculateFullscreenRect(GraphicsDevice graphics)
    {
        float aspectRatio = (float)GameWindow.Width / GameWindow.Height;

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