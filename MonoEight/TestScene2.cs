
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class TestScene2 : Scene
{
    public override void Awake()
    {
        Camera.BackgroundColor = Color.Purple;
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.IsKeyPressed(Keys.Space))
            SceneManager.Change("Test 1");

        base.Update(gameTime);
    }
}