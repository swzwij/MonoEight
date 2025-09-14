
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
        if (Input.IsKeyPressed(Keys.Space))
            SceneManager.Load("Test 2");

        if (Input.IsKeyPressed(Keys.Escape))
            SceneManager.Load("Loading");

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Canvas.DrawText(spriteBatch, "Test", FontSize.S, new(0, -20), MEColors.Red);
        Canvas.DrawText(spriteBatch, "test", FontSize.S, new(0, 0), MEColors.Red);
        Canvas.DrawText(spriteBatch, "lorem Ipsum is", FontSize.S, new(0, 10), MEColors.Red);
        Canvas.DrawText(spriteBatch, "simply dummy text.", FontSize.S, new(0, 20), MEColors.Red);

        Debugger.DrawPixel(spriteBatch, new(0, 0), MEColors.Black);
        Debugger.DrawPixel(spriteBatch, new(0, 5), MEColors.Blue);
        Debugger.DrawPixel(spriteBatch, new(0, -5), MEColors.Green);
        Debugger.DrawPixel(spriteBatch, new(0, -3), MEColors.Orange);

        base.Draw(spriteBatch);
    }
}