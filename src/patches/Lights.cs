using HarmonyLib;
using SCPE;
using UnityEngine;

namespace GraphicsMod.Patches {
    internal static class Lights {
        /**
         * <summary>
         * Applies sunglare settings.
         * If `-nosg` is provided in the launch options,
         * then this patch does nothing.
         * </summary>
         */
        private static void UpdateSunglare() {
            // Ignore when -nosg is provided
            if (DisplayOptions.sunglareLaunchOption == true) {
                return;
            }

            // Bad state
            if (Cache.postProcessGlobal == null) {
                return;
            }

            // Update the setting as needed
            if (Cache.postProcessGlobal.profile
                .TryGetSettings<Sunshafts>(out var setting) == false
            ) {
                return;
            }

            setting.enabled.value = Config.sunglare.Value;
        }

        /**
         * <summary>
         * Updates miscellaneous lights.
         * </summary>
         */
        private static void UpdateLights() {
            foreach (Cache.LightInfo info in Cache.lights) {
                info.light.enabled = (Config.lights.Value == true)
                    ? info.defaultEnabled
                    : false;

                info.light.shadows = (Config.shadows.Value == true)
                    ? info.defaultShadows
                    : LightShadows.None;
            }
        }

        /**
         * <summary>
         * Updates the sun, maybe.
         * </summary>
         */
        private static void UpdateDirectionalLights() {
            foreach (Cache.LightInfo info in Cache.directionalLights) {
                info.light.shadows = (Config.sunShadows.Value == true)
                    ? info.defaultShadows
                    : LightShadows.None;
            }
        }

        /**
         * <summary>
         * Iterates over lights in the scene and applies
         * the user's settings to them.
         * </summary>
         */
        internal static void SceneLoad() {
            UpdateSunglare();
            UpdateLights();
            UpdateDirectionalLights();
        }
    }
}
