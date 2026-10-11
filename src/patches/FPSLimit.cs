using HarmonyLib;
using UnityEngine;

namespace GraphicsMod.Patches {
    internal static class FPSLimiter {
        /**
         * <summary>
         * Takes complete control over fps limiting.
         * </summary>
         */
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Button), "LoadSavedVSync")]
        [HarmonyPatch(typeof(Button), "ToggleVerticalSync")]
        [HarmonyPatch(typeof(FPSLimit), "Start")]
        [HarmonyPatch(typeof(FPSLimit), "SetFPSLimit")]
        [HarmonyPatch(typeof(SetFramerate), "Awake")]
        private static void ApplyLimit() {
            // Unlimited fps
            if (Config.fpsLimitEnabled.Value == false) {
                Application.targetFrameRate = -1;
                return;
            }

            // Vsync
            if (Config.fpsVsync.Value == true) {
                QualitySettings.vSyncCount = 1;
                return;
            }

            // Custom limit
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = Config.fpsLimit.Value;
        }
    }
}
