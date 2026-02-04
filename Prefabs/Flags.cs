namespace A2.CleanLargeWoodGate.Prefabs
{
    internal static class Flags
    {
        public static bool Evaluate()
        {
            var result = false;
            result = (PluginConfig.DisableBloodOnMountainKitWoodGate is not null && Evaluate(ref MountainKitWoodGate, PluginConfig.DisableBloodOnMountainKitWoodGate.Value)) || result;
            return result;
        }

        private static bool Evaluate(ref PrefabState state, bool config)
        {
            // disabling is turned on in config, so prefab should be modified
            if (config)
            {
                state = PrefabState.ToModify;
                return true;
            }

            // disabling is turned off in config, so prefab should be restored to it's original state
            if (state == PrefabState.ToRestore)
            {
                // already marked to restore
                return true;
            }
            if (state == PrefabState.Modified)
            {
                // requires restoration
                state = PrefabState.ToRestore;
                return true;
            }
            if (state == PrefabState.ToModify)
            {
                // not yet modifed, mark as restored
                state = PrefabState.Restored;
                return false;
            }

            state = PrefabState.Unknown;
            return false;
        }

        public static PrefabState MountainKitWoodGate = PrefabState.Unknown;
    }
}
