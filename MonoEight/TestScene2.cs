namespace MonoEight;

public class TestScene2 : Scene
{
    private void Initialize()
    {
        Camera.BackgroundColor = MEColors.Blue;
        Add(new Player("PlayerTest"));
    }
}