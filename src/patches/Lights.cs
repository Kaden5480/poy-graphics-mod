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
         * Patches miscellaneous lights.
         * </summary>
         */
        private static void UpdateLight(Light light) {
            if (Config.lights.Value == false) {
                light.enabled = false;
            }

            if (Config.shadows.Value == false) {
                light.shadows = LightShadows.None;
            }
        }

        /**
         * <summary>
         * Patches the sun, maybe.
         * </summary>
         */
        private static void UpdateSun(Light light) {
            if (Config.sunShadows.Value == false) {
                light.shadows = LightShadows.None;
            }
        }

        /**
         * <summary>
         * Iterates over lights in the scene and applies
         * the user's settings to them.
         * </summary>
         */
        internal static void UpdateLights() {
            foreach (Light light in GameObject.FindObjectsOfType<Light>()) {
                // The sun, probably
                if (light.type == LightType.Directional) {
                    UpdateSun(light);
                    continue;
                }

                // Other lights
                UpdateLight(light);
            }

            UpdateSunglare();
        }
    }
}
