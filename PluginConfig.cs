using A2.CleanLargeWoodGate.Prefabs;
using A2.CleanLargeWoodGate.Prefabs.Code;
using BepInEx.Configuration;

namespace A2.CleanLargeWoodGate
{
    internal class PluginConfig
    {
        public static ConfigFile? File;

        public static ConfigEntry<bool>? DisableBloodOnMountainKitWoodGate;

        public static void Bind(ConfigFile config)
        {
            if (File is not null) return;
            File = config;

            DisableBloodOnMountainKitWoodGate = WithChangedHandler(config.Bind("Disable blood decals", MountainKitWoodGate.Name, true, MountainKitWoodGate.Name));
        }

        private static ConfigEntry<bool>? WithChangedHandler(ConfigEntry<bool>? configEntry)
        {
            if (configEntry is not null)
            {
                configEntry.SettingChanged += OnSettingsChanged;
            }
            return configEntry;
        }

        private static void OnSettingsChanged(object sender, System.EventArgs e) => Controller.Update();
    }
}
