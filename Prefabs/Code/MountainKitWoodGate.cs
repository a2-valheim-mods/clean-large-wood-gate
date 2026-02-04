using A2.CleanLargeWoodGate.Unity;
using System.Collections.Generic;
using UnityEngine;

namespace A2.CleanLargeWoodGate.Prefabs.Code
{
    internal static class MountainKitWoodGate
    {
        public const string Name = "Mountain kit wood gate";
        public const string PrefabName = "MountainKit_wood_gate";

        public static bool Modify(Dictionary<string, GameObject> prefabs)
        {
            try
            {
                if (Flags.MountainKitWoodGate != PrefabState.ToModify) return false;
#if DEBUG
                Jotunn.Logger.LogInfo($"{nameof(MountainKitWoodGate)}.{nameof(Modify)}: modifying state of the prefab {PrefabName}");
#endif
                if (!prefabs.TryGetValue(PrefabName, out var prefab))
                {
                    Jotunn.Logger.LogInfo($"{nameof(MountainKitWoodGate)}.{nameof(Modify)}: Prefab {PrefabName} not found.");
                    return false;
                }

                var result = true;
                result = prefab.SetChildrenInactive(
                    "CastleKit_decal_fenrir_blood",
                    "CastleKit_decal_fenrir_blood (1)",
                    "CastleKit_decal_fenrir_blood (2)",
                    "CastleKit_decal_fenrir_blood (3)"
                    ) && result;

                if (result) Flags.MountainKitWoodGate = PrefabState.Modified;
                return result;
            }
            catch (System.Exception ex)
            {
                Jotunn.Logger.LogError($"{nameof(MountainKitWoodGate)}.{nameof(Restore)}: Exception occurred:\n{ex}");
                return false;
            }
        }
        public static bool Restore(Dictionary<string, GameObject> prefabs)
        {
            try
            {
                if (Flags.MountainKitWoodGate != PrefabState.ToRestore) return false;
#if DEBUG
                Jotunn.Logger.LogInfo($"{nameof(MountainKitWoodGate)}.{nameof(Restore)}: restoring state of the prefab {PrefabName}");
#endif
                if (!prefabs.TryGetValue(PrefabName, out var prefab))
                {
                    Jotunn.Logger.LogInfo($"{nameof(MountainKitWoodGate)}.{nameof(Restore)}: Prefab {PrefabName} not found.");
                    return false;
                }

                var result = true;
                result = prefab.SetChildrenActive(
                    "CastleKit_decal_fenrir_blood",
                    "CastleKit_decal_fenrir_blood (1)",
                    "CastleKit_decal_fenrir_blood (2)",
                    "CastleKit_decal_fenrir_blood (3)"
                    ) && result;

                if (result) Flags.MountainKitWoodGate = PrefabState.Restored;
                return result;
            }
            catch (System.Exception ex)
            {
                Jotunn.Logger.LogError($"{nameof(MountainKitWoodGate)}.{nameof(Restore)}: Exception occurred:\n{ex}");
                return false;
            }
        }
    }
}
