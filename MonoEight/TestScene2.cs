namespace MonoEight;

public class TestScene2 : Scene
{
    protected override void Initialize()
    {
        Camera.BackgroundColor = MEColors.Blue;
        Add(new NewPlayer("PlayerTest"));
    }
}