namespace MonoEight;

public class TestScene2 : Scene
{
    public override void Awake()
    {
        Camera.BackgroundColor = MEColors.Blue;
        Add(new Player("PlayerTest"));
        base.Awake();
    }
}