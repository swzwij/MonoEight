using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEight.Core;
using MonoEight.Core.UserInput;
using MonoEight.Core.UI;
using MonoEight.Core.Scenes;

namespace Template;

public class MainMenuScene : Scene
{
    protected override void Initialize()
    {
        Camera.BackgroundColor = Color.Black;
    }

    protected override void Update()
    {
        if (Input.IsPressed("Exit"))
            SceneManager.Load("Loading");
    }

    protected override void Draw(SpriteBatch spriteBatch)
    {
        Canvas.DrawText(spriteBatch, "Main Menu Text", FontSize.M, new(0, -20), MEColors.Red);
        Canvas.DrawText(spriteBatch, "Sub Text", FontSize.S, new(0, 10), MEColors.Blue);
    }
}