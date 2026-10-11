using System.Collections.Generic;

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
        internal struct LightInfo {
            internal Light light;
            internal bool defaultEnabled;
            internal LightShadows defaultShadows;
        }

        internal static Scene scene { get; private set; }

        internal static PostProcessVolume postProcessGlobal { get; private set; }

        // All lights which aren't directional
        internal static List<LightInfo> lights { get; private set; }
            = new List<LightInfo>();

        // Directional lights
        internal static List<LightInfo> directionalLights { get; private set; }
            = new List<LightInfo>();

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

            lights.Clear();
            directionalLights.Clear();

            foreach (Light light in GameObject.FindObjectsOfType<Light>()) {
                LightInfo info = new LightInfo() {
                    light = light,
                    defaultEnabled = light.enabled,
                    defaultShadows = light.shadows,
                };

                if (light.type == LightType.Directional) {
                    directionalLights.Add(info);
                }
                else {
                    lights.Add(info);
                }

            }
        }

        /**
         * <summary>
         * Clears the cache on scene unloads.
         * </summary>
         */
        internal static void Clear() {
            postProcessGlobal = null;
            lights.Clear();
            directionalLights.Clear();
        }
    }
}
