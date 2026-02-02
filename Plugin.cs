using A2.CleanLargeWoodGate.Prefabs;
using BepInEx;
using HarmonyLib;
using Jotunn.Managers;
using Jotunn.Utils;
using UnityEngine;
using UnityEngine.Rendering;

namespace A2.CleanLargeWoodGate
{
    [BepInPlugin(PluginInfo.PluginGUID, PluginInfo.PluginName, PluginInfo.PluginSemanticVersion)]
    [BepInDependency(Jotunn.Main.ModGuid, BepInDependency.DependencyFlags.HardDependency)]
    [NetworkCompatibility(CompatibilityLevel.NotEnforced, VersionStrictness.None)]
    internal class Plugin : BaseUnityPlugin
    {
        private static readonly Harmony _harmony = new(PluginInfo.PluginGUID);
        private static bool _isInitialized = false;

        public void Awake()
        {
            if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null)
            {
                // do not run on dedicated server
                return;
            }
            if (_isInitialized)
            {
                return;
            }
            _isInitialized = true;

            _harmony.PatchAll();
            PluginConfig.Bind(Config);
            PrefabManager.OnVanillaPrefabsAvailable += OnVanillaPrefabsAvailable;
        }

        public void OnDestroy()
        {
            if (!_isInitialized)
            {
                return;
            }
            _isInitialized = false;

            PrefabManager.OnVanillaPrefabsAvailable -= OnVanillaPrefabsAvailable;
            _harmony.UnpatchSelf();
        }

        private static void OnVanillaPrefabsAvailable() => Controller.Update();
    }
}
