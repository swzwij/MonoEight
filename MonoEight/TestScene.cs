
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

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Canvas.DrawTest(spriteBatch, "Test", FontSize.S, new(0, 0), MEColors.Red);
        Debugger.DrawPixel(spriteBatch, Point.Zero, MEColors.DarkPurple);
        base.Draw(spriteBatch);
    }
}