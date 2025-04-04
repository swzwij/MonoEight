using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace MonoEight;

/// <summary>
/// ContentLoader is a utility class for loading and caching game content.
/// It uses a ContentManager to load content and caches it for later use.
/// </summary>
public static class ContentLoader
{
    private static ContentManager _content;
    private static readonly Dictionary<string, object> _contentCache = [];
    private static string _root;

    public static void Initialize(ContentManager content)
    {
        _content = content;
    }

    /// <summary>
    /// Loads content of type T from the specified path.
    /// If the content is already loaded, it will be retrieved from the cache.
    /// </summary>
    /// <typeparam name="T">The type of content to load.</typeparam>
    /// <param name="path">The path to the content.</param>
    /// <returns>The loaded content of type T.</returns>
    public static T Load<T>(string path)
    {
        if(_contentCache.TryGetValue(path, out object content) && content is T typedContent)
            return typedContent;

        T loadedContent = _content.Load<T>(path);
        return Cache(path, loadedContent);
    }

    /// <summary>
    /// Loads content of type T from the specified path using a different root directory.
    /// This is useful for loading content from a different directory than the default one.
    /// </summary>
    /// <typeparam name="T">The type of content to load.</typeparam>
    /// <param name="root">The root directory to load content from.</param>
    /// <param name="path">The path to the content.</param>
    public static T LoadFromRoot<T>(string root, string path)
    {
        _root = _content.RootDirectory;
        _content.RootDirectory = root;
        T loadedContent = _content.Load<T>(path);
        _content.RootDirectory = _root;
        return loadedContent;
    }

    private static T Cache<T>(string name, T content)
    {
        if (_contentCache.TryAdd(name, content))
            return content;

        throw new System.ArgumentException($"Content '{name}' of type {typeof(T).Name} already exists in the loader");
    }
}