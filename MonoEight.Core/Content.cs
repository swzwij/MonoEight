using Microsoft.Xna.Framework.Content;

namespace MonoEight.Core;

/// <summary>
/// Provides static methods for loading content assets using the MonoGame content builder.
/// </summary>
public static class Content
{
    private static ContentManager _contentManager;

    public static void Initialize(ContentManager contentManager, string root)
    {
        _contentManager = contentManager;
        _contentManager.RootDirectory = root;
    }

    public static T Load<T>(string path)
    {
        return _contentManager.Load<T>(path);
    }

    public static T LoadFromRoot<T>(string root, string path)
    {
        string _originalRoot = _contentManager.RootDirectory;
        _contentManager.RootDirectory = root;
        T loadedContent = _contentManager.Load<T>(path);
        _contentManager.RootDirectory = _originalRoot;
        return loadedContent;
    }
}
