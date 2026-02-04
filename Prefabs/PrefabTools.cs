using System;
using System.Collections.Generic;
using UnityEngine;

namespace A2.CleanLargeWoodGate.Prefabs
{
    internal static class PrefabTools
    {
        public static bool TryModify(
            IReadOnlyDictionary<string, GameObject> prefabs,
            IReadOnlyDictionary<string, GameObject[]> clones,
            string prefabName,
            ref PrefabState state,
            Func<GameObject, bool> modifyFunc,
            string callerClassName,
            string callerMethodName)
        {
            try
            {
                if (state != PrefabState.ToModify) return false;
#if DEBUG
                Jotunn.Logger.LogInfo($"{callerClassName}.{callerMethodName}: modifying state of the prefab {prefabName}");
#endif
                if (!prefabs.TryGetValue(prefabName, out var prefab))
                {
                    Jotunn.Logger.LogInfo($"{callerClassName}.{callerMethodName}: Prefab {prefabName} not found.");
                    return false;
                }

                var result = true;
                if (clones.TryGetValue(prefabName, out var prefabClones))
                {
                    foreach (var clone in prefabClones)
                    {
                        result = modifyFunc(clone) || result;
                    }
                }
                result = modifyFunc(prefab) && result;

                if (result) state = PrefabState.Modified;
                return result;
            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError($"{callerClassName}.{callerMethodName}: Exception occurred:\n{ex}");
                return false;
            }
        }
        public static bool TryRestore(
            IReadOnlyDictionary<string, GameObject> prefabs,
            IReadOnlyDictionary<string, GameObject[]> clones,
            string prefabName,
            ref PrefabState state,
            Func<GameObject, bool> restoreFunc,
            string callerClassName,
            string callerMethodName)
        {
            try
            {
                if (state != PrefabState.ToRestore) return false;
#if DEBUG
                Jotunn.Logger.LogInfo($"{callerClassName}.{callerMethodName}: restoring state of the prefab {prefabName}");
#endif
                if (!prefabs.TryGetValue(prefabName, out var prefab))
                {
                    Jotunn.Logger.LogInfo($"{callerClassName}.{callerMethodName}: Prefab {prefabName} not found.");
                    return false;
                }

                var result = true;
                if (clones.TryGetValue(prefabName, out var prefabClones))
                {
                    foreach (var clone in prefabClones)
                    {
                        result = restoreFunc(clone) || result;
                    }
                }
                result = restoreFunc(prefab) && result;

                if (result) state = PrefabState.Restored;
                return result;
            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError($"{callerClassName}.{callerMethodName}: Exception occurred:\n{ex}");
                return false;
            }
        }
    }
}
