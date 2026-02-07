using MonoEight.Core;
using MonoEight.Core.Scenes;

namespace Template;

public class TestScene : Scene
{
    protected override void Initialize()
    {
        Camera.BackgroundColor = MEColors.Blue;
        Add(new NewPlayer("PlayerTest", -16));
        Add(new NewPlayer2("PlayerTest", 16));
    }
}