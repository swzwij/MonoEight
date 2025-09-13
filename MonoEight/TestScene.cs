
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
        Canvas.DrawText(spriteBatch, "Test", FontSize.S, new(0, -20), MEColors.Red);
        Canvas.DrawText(spriteBatch, "test", FontSize.Test, new(0, 0), MEColors.Red);
        Canvas.DrawText(spriteBatch, "lorem Ipsum is simply dummy text", FontSize.Test, new(0, 10), MEColors.Red);
        Canvas.DrawText(spriteBatch, "of the printing and typesetting industry.", FontSize.Test, new(0, 20), MEColors.Red);
        base.Draw(spriteBatch);
    }
}