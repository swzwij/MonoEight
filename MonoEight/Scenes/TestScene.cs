
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class TestScene : Scene
{
    public override void Awake()
    {
        Camera.BackgroundColor = Color.LightYellow;

        base.Awake();
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.IsPressed("A"))
            SceneManager.Load("Test 2");

        if (Input.IsPressed("Exit"))
            SceneManager.Load("Loading");

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Canvas.DrawText(spriteBatch, "Test", FontSize.S, new(0, -20), MEColors.Red);
        Canvas.DrawText(spriteBatch, "Test", FontSize.M, new(0, 0), MEColors.Red);
        Canvas.DrawText(spriteBatch, "Lorem Ipsum is", FontSize.S, new(0, 10), MEColors.Red);
        Canvas.DrawText(spriteBatch, "Simply dummy text.", FontSize.M, new(0, 20), MEColors.Red);

        Debugger.DrawPixel(spriteBatch, new(0, 0), MEColors.Black);
        Debugger.DrawPixel(spriteBatch, new(0, 5), MEColors.Blue);
        Debugger.DrawPixel(spriteBatch, new(0, -5), MEColors.Green);
        Debugger.DrawPixel(spriteBatch, new(0, -3), MEColors.Orange);

        base.Draw(spriteBatch);
    }
}