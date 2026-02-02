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
            if (config)
            {
                state = PrefabState.ToModify;
                return true;
            }

            if (state == PrefabState.ToRestore)
            {
                return false;
            }
            if (state == PrefabState.Modified)
            {
                state = PrefabState.ToRestore;
                return true;
            }

            state = PrefabState.Unknown;
            return false;
        }

        public static PrefabState MountainKitWoodGate = PrefabState.Unknown;
    }
}
