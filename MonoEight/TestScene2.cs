namespace MonoEight;

public class TestScene2 : Scene
{
    public override void Awake()
    {
        Add(new Player("PlayerTest"));
        base.Awake();
    }
}