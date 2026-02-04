using A2.CleanLargeWoodGate.Unity;
using System.Collections.Generic;
using UnityEngine;
using static A2.CleanLargeWoodGate.Prefabs.PrefabTools;

namespace A2.CleanLargeWoodGate.Prefabs.Code
{
    internal static class MountainKitWoodGate
    {
        public const string Name = "Mountain kit wood gate";
        public const string PrefabName = "MountainKit_wood_gate";

        public static bool Modify(IReadOnlyDictionary<string, GameObject> prefabs, IReadOnlyDictionary<string, GameObject[]> clones)
            => TryModify(prefabs, clones, PrefabName, ref Flags.MountainKitWoodGate, Modify, nameof(MountainKitWoodGate), nameof(Modify));
        public static bool Restore(IReadOnlyDictionary<string, GameObject> prefabs, IReadOnlyDictionary<string, GameObject[]> clones)
            => TryRestore(prefabs, clones, PrefabName, ref Flags.MountainKitWoodGate, Restore, nameof(MountainKitWoodGate), nameof(Restore));

        private static bool Modify(GameObject prefab)
        {
            var result = true;
            result = prefab.SetChildrenInactive(
                "CastleKit_decal_fenrir_blood",
                "CastleKit_decal_fenrir_blood (1)",
                "CastleKit_decal_fenrir_blood (2)",
                "CastleKit_decal_fenrir_blood (3)"
                ) && result;
            return result;
        }
        private static bool Restore(GameObject prefab)
        {
            var result = true;
            result = prefab.SetChildrenActive(
                "CastleKit_decal_fenrir_blood",
                "CastleKit_decal_fenrir_blood (1)",
                "CastleKit_decal_fenrir_blood (2)",
                "CastleKit_decal_fenrir_blood (3)"
                ) && result;
            return result;
        }
    }
}
