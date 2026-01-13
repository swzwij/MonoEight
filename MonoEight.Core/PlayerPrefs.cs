using System.Text.Json;

namespace MonoEight.Core;

/// <summary>
/// Provides a static interface for storing and retrieving simple player data to a local JSON file.
/// </summary>
/// <remarks>
/// Supported Types: int, double, bool, string. 
/// </remarks>
public static class PlayerPrefs
{
    private static readonly Dictionary<string, object?> _prefs = [];
    private static string? _path;
    private static readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };
    
    /// <summary>
    /// Initializes the storage directory and loads existing preferences from disk.
    /// </summary>
    /// <remarks>
    /// <para><b>File Locations:</b></para>
    /// <list type="bullet">
    /// <item>Windows: <c>%AppData%\MonoEight-Game\playerprefs.json</c></item>
    /// <item>Unix/Mac: <c>~/.local/share/MonoEight-Game/playerprefs.json</c></item>
    /// </list>
    /// </remarks>
    public static void Initialize()
    {
        string root = Environment.OSVersion.Platform == PlatformID.Unix
            ? Path.Combine(Environment.GetEnvironmentVariable("HOME") ?? "", ".local", "share", "MonoEight-Game")
            : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MonoEight-Game");

        Directory.CreateDirectory(root);
        _path = Path.Combine(root, "playerprefs.json");
        Load();
    }

    private static void Load()
    {
        if (!File.Exists(_path))
            return;

        try
        {
            string json = File.ReadAllText(_path);
            Dictionary<string, JsonElement>? loadedPrefs = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);

            _prefs.Clear();
            
            if (loadedPrefs == null)
                return;
            
            foreach (KeyValuePair<string, JsonElement> pair in loadedPrefs)
            {
                switch (pair.Value.ValueKind)
                {
                    case JsonValueKind.Number:
                        if (pair.Value.TryGetInt32(out int intValue))
                            _prefs[pair.Key] = intValue;
                        else if (pair.Value.TryGetDouble(out double doubleValue))
                            _prefs[pair.Key] = doubleValue;
                        break;
                    case JsonValueKind.String:
                        _prefs[pair.Key] = pair.Value.GetString();
                        break;
                    case JsonValueKind.True:
                    case JsonValueKind.False:
                        _prefs[pair.Key] = pair.Value.GetBoolean();
                        break;
                    case JsonValueKind.Undefined:
                    case JsonValueKind.Object:
                    case JsonValueKind.Array:
                    case JsonValueKind.Null:
                    default:
                        break;
                }
            }
        }
        catch (Exception e)
        {
            _prefs.Clear();
            throw new Exception($"Error loading preferences: {e.Message}", e);
        }
    }

    private static void Save()
    {
        try
        {
            string json = JsonSerializer.Serialize(_prefs, _jsonOptions);

            if (_path == null)
                return;
            
            File.WriteAllText(_path, json);
        }
        catch (Exception e)
        {
            throw new Exception($"Error saving preferences: {e.Message}", e);
        }
    }

    private static T GetValue<T>(string key, T defaultValue)
    {
        if (_prefs.TryGetValue(key, out object? value) && value is T typedValue)
            return typedValue;

        return defaultValue;
    }

    /// <summary>
    /// Retrieves the value associated with the given key.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="key">The unique key to identify the preference.</param>
    /// <param name="defaultValue">The value to return if the key does not exist.</param>
    /// <returns>The stored value, or <paramref name="defaultValue"/> if not found.</returns>
    public static T? Get<T>(string key, T? defaultValue = default)
    {
        return GetValue(key, defaultValue);
    }

    /// <summary>
    /// Sets the value for the given key and writes the changes to disk immediately.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="key">The unique key.</param>
    /// <param name="value">The value to store.</param>
    public static void Set<T>(string key, T value)
    {
        _prefs[key] = value;
        Save();
    }

    /// <summary>
    /// Checks if the given key exists in the preferences.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns><c>true</c> if the key exists, otherwise, <c>false</c>.</returns>
    public static bool HasKey(string key)
    {
        return _prefs.ContainsKey(key);
    }

    /// <summary>
    /// Removes the given key and its value from the preferences, then saves to disk.
    /// </summary>
    /// <param name="key">The key to remove.</param>
    public static void DeleteKey(string key)
    {
        if (_prefs.Remove(key))
            Save();
    }

    /// <summary>
    /// Removes all keys and values from the preferences, then saves to disk.
    /// </summary>
    public static void DeleteAll()
    {
        _prefs.Clear();
        Save();
    }
}
