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
        if (!MapAvailable(map, out InputActionMap actionMap)) return;

        if (enable) actionMap.Enable();
        else actionMap.Disable();
    }
    public static void ModifyOnly(GameMap map, bool enable)
    {
        if (!MapAvailable(map, out InputActionMap actionMap)) return;

        if (enable) { ModifyAll(!enable); actionMap.Enable(); }
        else { ModifyAll(enable); actionMap.Disable(); }
    }
    public static void ModifyAll(bool enable)
    {
        foreach (var pairs in mapDictionary)
        {
            if (pairs.Value == null) continue;

            if (enable) pairs.Value.Enable();
            else pairs.Value.Disable();
        }
    }
    public static bool MapEnabled(GameMap map)
    {
        if (!MapAvailable(map, out InputActionMap actionMap)) return false;

        return actionMap.enabled;
    }

    private static bool MapAvailable(GameMap map, out InputActionMap actionMap)
    {
        if (!mapDictionary.TryGetValue(map, out actionMap) || actionMap == null) return false;

        return true;
    }
    #endregion
}
