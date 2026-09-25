using UnityEngine;
using UnityEngine.InputSystem;
using System;

public enum GameMap { Player, UI, }

public static class InputManager
{
    public static Controls Controls { get; private set; } = new();
    private static readonly System.Collections.Generic.Dictionary<GameMap, InputActionMap> mapDictionary = new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        Controls.Enable();

        mapDictionary.Clear();
        mapDictionary.Add(GameMap.Player, Controls.Player);
        mapDictionary.Add(GameMap.UI, Controls.UI);
    }

    #region Map settings
    public static void ModifyMap(GameMap map, bool enable)
    {
        if (!mapDictionary.TryGetValue(map, out InputActionMap actionMap) || actionMap == null) return;

        if (enable) actionMap.Enable(); else actionMap.Disable();
    }
    public static void ModifyOnly(GameMap map, bool enable) { ModifyAll(!enable); ModifyMap(map, enable); }
    public static void ModifyAll(bool enable) { foreach (var pairs in mapDictionary) ModifyMap(pairs.Key, enable); }
    public static bool MapEnabled(GameMap map)
    {
        if (!mapDictionary.TryGetValue(map, out InputActionMap actionMap) || actionMap == null) return false;

        return actionMap.enabled;
    }
    #endregion
}
