using System.Collections.Generic;
using System.Reflection;

namespace MonoEight.Internal;

public abstract class MessageReceiver
{
    private Dictionary<string, MethodInfo> _messageHandlers;

    private static readonly HashSet<string> _magicMethods =
    [
        "Initialize",
        "LoadContent",
        "Update",
        "Draw",
        "Unload",
    ];

    protected MessageReceiver()
    {
        DiscoverMessageHandlers();
    }

    private void DiscoverMessageHandlers()
    {
        _messageHandlers = [];

        MethodInfo[] methods = GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (MethodInfo method in methods)
        {
            if (!IsValidMethod(method))
                continue;

            _messageHandlers[method.Name] = method;
        }
    }

    public void SendMessage(string messageName, params object[] parameters)
    {
        if (!_messageHandlers.TryGetValue(messageName, out MethodInfo handler))
            return;

        ParameterInfo[] parameterTypes = handler.GetParameters();

        if (parameterTypes.Length != parameters.Length)
            return;

        handler.Invoke(this, parameters);
    }

    private bool IsValidMethod(MethodInfo method)
    {
        bool hasName = _magicMethods.Contains(method.Name);
        bool hasType = method.ReturnType == typeof(void);
        bool hasDeclaringType = method.DeclaringType != typeof(MessageReceiver);
        return hasName && hasType && hasDeclaringType;
    }
}