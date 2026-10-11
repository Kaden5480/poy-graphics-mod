using BepInEx.Configuration;
using ModMenu.Config;
using UnityEngine;

namespace GraphicsMod {
    internal static class Config {
        // General
        [Field("Shown Warning")]
        internal static ConfigEntry<bool> shownWarning { get; private set; }

        // FPS Limit
        [Field("Enabled")]
        [Listener(typeof(Patches.FPSLimiter), "ApplyLimit")]
        internal static ConfigEntry<bool> fpsLimitEnabled { get; private set; }

        [Field("Limit", min=15, max=500)]
        [Listener(typeof(Patches.FPSLimiter), "ApplyLimit")]
        internal static ConfigEntry<int> fpsLimit { get; private set; }

        [Field("Vsync")]
        [Listener(typeof(Patches.FPSLimiter), "ApplyLimit")]
        internal static ConfigEntry<bool> fpsVsync { get; private set; }

        // Lighting
        [Field("Lights")]
        [Listener(typeof(Patches.Lights), "UpdateLights")]
        internal static ConfigEntry<bool> lights { get; private set; }

        [Field("Shadows")]
        [Listener(typeof(Patches.Lights), "UpdateLights")]
        internal static ConfigEntry<bool> shadows { get; private set; }

        [Field("Sun Shadows")]
        [Listener(typeof(Patches.Lights), "UpdateDirectionalLights")]
        internal static ConfigEntry<bool> sunShadows { get; private set; }

        [Field("Sunglare")]
        [Listener(typeof(Patches.Lights), "UpdateSunglare")]
        internal static ConfigEntry<bool> sunglare { get; private set; }

        /**
         * <summary>
         * Initializes the config by binding to the
         * provided `ConfigFile`.
         * </summary>
         */
        internal static void Init(ConfigFile configFile) {
            // General
            shownWarning = configFile.Bind(
                "General", "shownWarning", false,
                "Whether the warning was shown to suggest checking"
                + " the graphics mod settings."
            );

            // FPS Limit
            fpsLimitEnabled = configFile.Bind(
                "FPS Limit", "enabled", true,
                "Whether to limit the FPS. When this option is disabled,"
                + " the FPS will be unlimited."
            );

            fpsLimit = configFile.Bind(
                "FPS Limit", "limit", 60,
                "The FPS limit to apply."
            );

            fpsVsync = configFile.Bind(
                "FPS Limit", "vsync", true,
                "Whether vsync should be enabled. If this option is enabled,"
                + " then your configured FPS limit will be ignored."
            );

            // Lighting
            lights = configFile.Bind(
                "Lighting", "lights", true,
                "Whether lights should be enabled. This option does not"
                + " affect the sun."
            );

            shadows = configFile.Bind(
                "Lighting", "shadows", true,
                "Whether shadows should be enabled. This option does not"
                + " affect the sun's shadows."
            );

            sunShadows = configFile.Bind(
                "Lighting", "sunShadows", true,
                "Whether the sun's shadows should be enabled."
            );

            sunglare = configFile.Bind(
                "Lighting", "sunglare", true,
                "Whether sunglare should be enabled. This will not"
                + " override the \"-nosg\" launch option."
            );
        }
    }
}
