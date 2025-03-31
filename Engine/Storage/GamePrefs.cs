using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MonoEight;

/// <summary>
/// Handles game preferences storage and retrieval.
/// This class provides methods to set, get, and delete preferences in a JSON file.
/// </summary>
public static class GamePrefs
{
    private static Dictionary<string, object> _prefs = [];
    private static string _filePath;

    /// <summary>
    /// Initializes the GamePrefs system.
    /// </summary>
    /// <param name="gameName">The name of the game.</param>
    public static void Initialize(string gameName)
    {
        string rootDirectory;

        if (Environment.OSVersion.Platform == PlatformID.Unix)
        {
            rootDirectory = Path.Combine // Linux
            (
                Environment.GetEnvironmentVariable("HOME"),
                ".local", "share", gameName
            );
        }
        else
        {
            rootDirectory = Path.Combine // Windows
            (
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                gameName
            );
        }

        if (!Directory.Exists(rootDirectory))
            Directory.CreateDirectory(rootDirectory);

        _filePath = Path.Combine(rootDirectory, "playerprefs.json");

        Console.WriteLine($"GamePrefs file path: {_filePath}");

        Load();
    }

    private static void Load()
    {
        if (!File.Exists(_filePath))
            return;

        try
        {
            string json = File.ReadAllText(_filePath);
            Dictionary<string, JsonElement> loadedPrefs = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);

            _prefs.Clear();
            foreach (KeyValuePair<string, JsonElement> pair in loadedPrefs)
            {
                switch (pair.Value.ValueKind)
                {
                    case JsonValueKind.Number:
                        if (pair.Value.TryGetInt32(out int intValue))
                            _prefs[pair.Key] = intValue;
                        else
                            _prefs[pair.Key] = pair.Value.GetDouble();
                        break;
                    case JsonValueKind.String:
                        _prefs[pair.Key] = pair.Value.GetString();
                        break;
                    case JsonValueKind.True:
                    case JsonValueKind.False:
                        _prefs[pair.Key] = pair.Value.GetBoolean();
                        break;
                    default:
                        break;
                }
            }
        }
        catch (Exception e)
        {
            _prefs = [];
            throw new Exception($"Error loading preferences: {e.Message}", e);
        }
    }

    private static void Save()
    {
        try
        {
            string json = JsonSerializer.Serialize(_prefs);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception e)
        {
            throw new Exception($"Error saving preferences: {e.Message}", e);
        }
    }

    /// <summary>
    /// Sets a preference value.
    /// </summary>
    /// <param name="key">The key of the preference.</param>
    /// <param name="value">The value to set.</param>
    public static void Set(string key, object value)
    {
        _prefs[key] = value;
        Save();
    }

    /// <summary>
    /// Sets an integer preference value.
    /// </summary>
    /// <param name="key">The key of the preference.</param>
    /// <param name="value">The integer value to set.</param>
    public static void SetInt(string key, int value)
    {
        _prefs[key] = value;
        Save();
    }

    /// <summary>
    /// Sets a float preference value.
    /// </summary>
    /// <param name="key">The key of the preference.</param>
    /// <param name="value">The float value to set.</param>
    public static void SetFloat(string key, float value)
    {
        _prefs[key] = value;
        Save();
    }

    /// <summary>
    /// Sets a string preference value.
    /// </summary>
    /// <param name="key">The key of the preference.</param>
    /// <param name="value">The string value to set.</param>
    public static void SetString(string key, string value)
    {
        _prefs[key] = value;
        Save();
    }

    /// <summary>
    /// Sets a boolean preference value.
    /// </summary>
    /// <param name="key">The key of the preference.</param>
    /// <param name="value">The boolean value to set.</param>
    public static void SetBool(string key, bool value)
    {
        _prefs[key] = value;
        Save();
    }

    /// <summary>
    /// Gets a preference value.
    /// </summary>
    /// <param name="key">The key of the preference.</param>
    /// <returns>The value associated with the key, or null if not found.</returns>
    public static object Get(string key)
    {
        if (_prefs.TryGetValue(key, out object value))
            return value;

        Console.WriteLine($"Key '{key}' not found. Returning null.");
        return null;
    }

    /// <summary>
    /// Gets an integer preference value.
    /// </summary>
    /// <param name="key">The key of the preference.</param>
    public static int GetInt(string key)
    {
        if (_prefs.TryGetValue(key, out object value) && value is int intValue)
            return intValue;

        Console.WriteLine($"Key '{key}' not found or not an int. Returning -1.");
        return -1;
    }

    /// <summary>
    /// Gets a float preference value.
    /// </summary>
    /// <param name="key">The key of the preference.</param>
    public static float GetFloat(string key)
    {
        if (_prefs.TryGetValue(key, out object value))
        {
            if (value is float floatValue)
                return floatValue;
            else if (value is double doubleValue)
                return (float)doubleValue;
            else if (value is int intValue)
                return intValue;
        }

        Console.WriteLine($"Key '{key}' not found or not a float. Returning -1.");
        return -1;
    }

    /// <summary>
    /// Gets a string preference value.
    /// </summary>
    /// <param name="key">The key of the preference.</param>
    public static string GetString(string key)
    {
        if (_prefs.TryGetValue(key, out object value) && value is string stringValue)
            return stringValue;

        Console.WriteLine($"Key '{key}' not found or not a string. Returning empty string.");
        return string.Empty;
    }

    /// <summary>
    /// Gets a boolean preference value.
    /// </summary>
    /// <param name="key">The key of the preference.</param>
    public static bool GetBool(string key)
    {
        if (_prefs.TryGetValue(key, out object value) && value is bool boolValue)
            return boolValue;

        Console.WriteLine($"Key '{key}' not found or not a bool. Returning false.");
        return false;;
    }

    /// <summary>
    /// Checks if a preference key exists.
    /// </summary>
    /// <param name="key">The key of the preference.</param>
    public static bool HasKey(string key)
    {
        return _prefs.ContainsKey(key);
    }

    /// <summary>
    /// Deletes a preference key.
    /// </summary>
    /// <param name="key">The key of the preference to delete.</param>
    public static void DeleteKey(string key)
    {
        if (!_prefs.Remove(key))
            return;

        Save();
    }

    /// <summary>
    /// Deletes all preference keys.
    /// </summary>
    public static void DeleteAll()
    {
        _prefs.Clear();
        Save();
    }
}