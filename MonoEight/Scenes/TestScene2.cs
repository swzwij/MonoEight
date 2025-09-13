
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class TestScene2 : Scene
{
    public override void Awake()
    {
        Camera.BackgroundColor = MEColors.Black;
        base.Awake();
    }

    public override void LoadContent()
    {
        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.IsKeyPressed(Keys.Space))
            SceneManager.Load("Test 1");

        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}