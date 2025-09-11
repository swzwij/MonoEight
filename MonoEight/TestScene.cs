
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoEight;

public class TestScene : Scene
{
    public override void Awake()
    {
        Camera.BackgroundColor = Color.LightYellow;
    }

    public override void Update(GameTime gameTime)
    {
        if (Input.IsKeyPressed(Keys.Space))
            SceneManager.Change("Test 2");

        base.Update(gameTime);
    }
}