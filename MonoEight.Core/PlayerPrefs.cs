using System.Text.Json;

namespace MonoEight.Core;

/// <summary>
/// Provides a simple, static API for storing and retrieving user preferences and settings as key value pairs, persisted
/// to disk between application sessions.
/// </summary>
public static class PlayerPrefs
{
    private static readonly Dictionary<string, object?> _prefs = [];
    private static string? _path;
    private static readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };


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

    public static T? Get<T>(string key, T? defaultValue = default)
    {
        return GetValue(key, defaultValue);
    }

    public static void Set<T>(string key, T value)
    {
        _prefs[key] = value;
        Save();
    }

    public static bool HasKey(string key)
    {
        return _prefs.ContainsKey(key);
    }

    public static void DeleteKey(string key)
    {
        if (_prefs.Remove(key))
            Save();
    }

    public static void DeleteAll()
    {
        _prefs.Clear();
        Save();
    }
}
