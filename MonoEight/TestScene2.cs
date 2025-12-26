namespace MonoEight;

public class TestScene2 : Scene
{
    protected override void Initialize()
    {
        Camera.BackgroundColor = MEColors.Blue;
        Add(new NewPlayer("PlayerTest", 0));
        Add(new NewPlayer("PlayerTest", 32));
    }
}