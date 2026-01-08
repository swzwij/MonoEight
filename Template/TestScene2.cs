using MonoEight.Core;
using MonoEight.Core.Scenes;

namespace MonoEight;

public class TestScene2 : Scene
{
    protected override void Initialize()
    {
        Camera.BackgroundColor = MEColors.Blue;
        Add(new NewPlayer("PlayerTest", -16));
        Add(new NewPlayer2("PlayerTest", 16));
    }
}