using UnityEngine;
using UnityEngine.InputSystem;
using System;

/// <summary>Stores input action map keys that <c><see cref="InputManager"/></c> is allowed to manipulate.</summary>
public enum GameMap { Player, UI, }

/// <summary>Class responsible for managing input-specific actions.</summary>
public static class InputManager
{
    /// <summary>Generated script based on the Controls input action asset.</summary>
    public static Controls Controls { get; } = new();
    /// <summary>Stores registered input action maps for manipulation.</summary>
    private static readonly System.Collections.Generic.Dictionary<GameMap, InputActionMap> mapDictionary = new();

    /// <summary>Enables <c><see cref="Controls"/></c> and registers input action maps.</summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        Controls.Enable();

        mapDictionary.Clear();
        mapDictionary.Add(GameMap.Player, Controls.Player);
        mapDictionary.Add(GameMap.UI, Controls.UI);
    }

    #region Map settings
    /// <summary>Enable or disable the map of interest.</summary>
    /// <param name="map">Map of interest.</param>
    /// <param name="enable">True to enable, false to disable.</param>
    public static void ModifyMap(GameMap map, bool enable)
    {
        if (!mapDictionary.TryGetValue(map, out InputActionMap actionMap) || actionMap == null) return;

        if (enable) actionMap.Enable(); else actionMap.Disable();
    }
    /// <summary>Enable or disable all action maps, the map of interest gets the reverse condition.</summary>
    /// <param name="map">Map of interest.</param>
    /// <param name="enable">True to enable, false to disable.</param>
    public static void ModifyOnly(GameMap map, bool enable) { ModifyAll(!enable); ModifyMap(map, enable); }
    /// <summary>Enable or disable all maps.</summary>
    /// <param name="enable">True to enable, false to disable.</param>
    public static void ModifyAll(bool enable) { foreach (var pairs in mapDictionary) ModifyMap(pairs.Key, enable); }
    /// <summary>Checks whether the map of interest is enabled.</summary>
    /// <param name="map">The map of interest.</param>
    /// <returns>returns true if the map is found and enabled, otherwise false</returns>
    public static bool MapEnabled(GameMap map)
    {
        if (!mapDictionary.TryGetValue(map, out InputActionMap actionMap) || actionMap == null) return false;

        return actionMap.enabled;
    }
    #endregion
}
