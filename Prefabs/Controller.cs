using A2.CleanLargeWoodGate.Prefabs.Code;
using Jotunn.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace A2.CleanLargeWoodGate.Prefabs
{
    internal static class Controller
    {
        private static readonly HashSet<string> Names =
        [
            MountainKitWoodGate.PrefabName,
        ];

        private static Dictionary<string, GameObject> Find()
        {
            var prefabs = new Dictionary<string, GameObject>();
            foreach (var name in Names)
            {
                var prefab = PrefabManager.Instance.GetPrefab(name);
                if (prefab is null) continue;
                if (string.IsNullOrEmpty(prefab.name)) continue;

                var prefabName = prefab.name;
                prefabs.Add(prefabName, prefab);
            }
            return prefabs;
        }
        private static bool Modify(Dictionary<string, GameObject> prefabs)
        {
            var result = true;
            result = MountainKitWoodGate.Modify(prefabs) && result;
            return result;
        }
        private static bool Restore(Dictionary<string, GameObject> prefabs)
        {
            var result = true;
            result = MountainKitWoodGate.Restore(prefabs) && result;
            return result;
        }

        public static bool Update()
        {
            if (!Flags.Evaluate())
            {
                return false;
            }
            var prefabs = Find();
            if (prefabs.Count == 0)
            {
                return false;
            }
            var result = false;
            result = Restore(prefabs) || result;
            result = Modify(prefabs) || result;
            return result;
        }
    }
}
