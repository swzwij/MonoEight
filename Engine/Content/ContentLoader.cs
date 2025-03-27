using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace MonoEight;

public static class ContentLoader
{
    private static ContentManager _content;
    private static readonly Dictionary<string, object> _contentCache = [];
    private static string _root;

    public static void Initialize(ContentManager content)
    {
        _content = content;
    }

    public static T Load<T>(string path)
    {
        if(_contentCache.TryGetValue(path, out object content) && content is T typedContent)
            return typedContent;

        T loadedContent = _content.Load<T>(path);
        return Cache(path, loadedContent);
    }

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