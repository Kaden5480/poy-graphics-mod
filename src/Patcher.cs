using System;

using HarmonyLib;

namespace GraphicsMod {
    internal static class Patcher {
        private static Logger logger = new Logger(typeof(Patcher));

        /**
         * <summary>
         * Applies an individual patch and logs it.
         * </summary>
         */
        internal static void Patch(Type patch) {
            Harmony.CreateAndPatchAll(patch);
            logger.LogDebug($"Applied patch: {patch}");
        }

        /**
         * <summary>
         * Applies all patches.
         * </summary>
         */
        internal static void PatchAll() {
            Patch(typeof(Patches.FPSLimiter));
        }

        /**
         * <summary>
         * Runs patches which need to run on a scene load.
         * </summary>
         */
        internal static void SceneLoad() {
            Patches.Lights.UpdateLights();
        }
    }
}
