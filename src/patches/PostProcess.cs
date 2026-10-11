namespace GraphicsMod.Patches {
    internal static class PostProcess {
        private static void UpdatePostProcess() {
            if (Cache.postProcessGlobal == null) {
                return;
            }

            Cache.postProcessGlobal.enabled
                = Config.postProcessEnabled.Value;
        }

        internal static void SceneLoad() {
            UpdatePostProcess();
        }
    }
}
