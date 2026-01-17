using MonoEight.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoEight.Core.Scenes;
using MonoEight.Core.UserInput;

namespace MonoEight;

public class MyGame : MEGame
{
    protected override void OnGameInitialize()
    {
        MEWindow.StartFullscreen = false;
        MEWindow.Resolution = new Point(128, 96);

        Input.Add("Exit", [Keys.Escape, Keys.Back], []);
        Input.Add("A", [Keys.Z, Keys.C, Keys.K, Keys.Space], []);
        Input.Add("B", [Keys.X, Keys.L], []);

        SceneManager.Add("Test 1", new TestScene());
        SceneManager.Add("Test 2", new TestScene2());
    }
}
