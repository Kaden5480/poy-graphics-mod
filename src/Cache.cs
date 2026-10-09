using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace GraphicsMod {
    /**
     * <summary>
     * Caches useful objects on scene loads.
     * </summary>
     */
    internal static class Cache {
        internal static Scene scene { get; private set; }

        internal static PostProcessVolume postProcessGlobal { get; private set; }

        /**
         * <summary>
         * Finds objects on scene loads.
         * </summary>
         */
        internal static void FindObjects(Scene scene) {
            Cache.scene = scene;

            GameObject postProcessGlobalObj = GameObject.Find("_PostProcessingGlobal");
            if (postProcessGlobalObj != null) {
                postProcessGlobal = postProcessGlobalObj.GetComponent<PostProcessVolume>();
            }
        }

        /**
         * <summary>
         * Clears the cache on scene unloads.
         * </summary>
         */
        internal static void Clear() {
            postProcessGlobal = null;
        }
    }
}
