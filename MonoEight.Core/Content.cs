using Microsoft.Xna.Framework.Content;

namespace MonoEight.Core;

/// <summary>
/// Provides static methods for loading content assets using the MonoGame content builder.
/// </summary>
public static class Content
{
    private static ContentManager _contentManager = null!;

    /// <summary>
    /// Initializes the static content manager.
    /// </summary>
    /// <param name="contentManager">The <see cref="ContentManager"/> instance from the main Game class.</param>
    /// <param name="root">The root directory path (typically "Content").</param>
    public static void Initialize(ContentManager contentManager, string root)
    {
        _contentManager = contentManager;
        _contentManager.RootDirectory = root;
    }

    /// <summary>
    /// Loads an asset of type <typeparamref name="T"/> from the default content root directory.
    /// </summary>
    /// <typeparam name="T">The type of asset to load.</typeparam>
    /// <param name="path">The path to the asset, relative to the content root.</param>
    /// <returns>The loaded asset.</returns>
    public static T Load<T>(string path)
    {
        return _contentManager.Load<T>(path);
    }

    /// <summary>
    /// Loads an asset by temporarily switching the ContentManager's root directory to a different location.
    /// </summary>
    /// <remarks>
    /// This restores the original root directory after loading is complete.
    /// </remarks>
    /// <typeparam name="T">The type of asset to load.</typeparam>
    /// <param name="root">The temporary root directory to load from.</param>
    /// <param name="path">The path to the asset within that root.</param>
    public static T LoadFromRoot<T>(string root, string path)
    {
        string originalRoot = _contentManager.RootDirectory;
        _contentManager.RootDirectory = root;
        T loadedContent = _contentManager.Load<T>(path);
        _contentManager.RootDirectory = originalRoot;
        return loadedContent;
    }
}
