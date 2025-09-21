using MonoEight.Internal;

namespace MonoEight;

public class Component : MessageReceiver
{
    public GameObject GameObject { get; protected set; }
    public bool IsActive { get; set; } = true;

    public Component(GameObject gameObject)
    {
        GameObject = gameObject;
        GameObject.Add(this);
    }
}