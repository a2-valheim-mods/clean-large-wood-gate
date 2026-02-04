using A2.CleanLargeWoodGate.Prefabs.Code;
using Jotunn.Managers;
using System.Collections.Generic;
using System.Linq;
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
        private static Dictionary<string, GameObject[]> FindClones()
        {
            var allTransforms = Object.FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            return allTransforms
                .Select(t => t.gameObject)
                .Where(go =>
                {
                    var originalName = go.name.EndsWith("(Clone)")
                        ? go.name.Substring(0, go.name.Length - 7)
                        : go.name;
                    return Names.Contains(originalName);
                })
                .GroupBy(go => go.name.EndsWith("(Clone)")
                        ? go.name.Substring(0, go.name.Length - 7)
                        : go.name)
                .ToDictionary(g => g.Key, g => g.ToArray());
        }
        private static bool Modify(Dictionary<string, GameObject> prefabs, Dictionary<string, GameObject[]> clones)
        {
            var result = true;
            result = MountainKitWoodGate.Modify(prefabs, clones) && result;
            return result;
        }
        private static bool Restore(Dictionary<string, GameObject> prefabs, Dictionary<string, GameObject[]> clones)
        {
            var result = true;
            result = MountainKitWoodGate.Restore(prefabs, clones) && result;
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
            var clones = FindClones();
            var result = false;
            result = Restore(prefabs, clones) || result;
            result = Modify(prefabs, clones) || result;
            return result;
        }
    }
}
