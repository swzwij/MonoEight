using MonoEight.Internal;

namespace MonoEight;

public class Component : MessageReceiver
{
    public GameObject GameObject { get; protected set; }
    public bool IsActive { get; set; } = true;
}