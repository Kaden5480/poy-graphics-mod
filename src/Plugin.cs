using System;
using System.Linq;

using BepInEx;
using HarmonyLib;
using ModMenu;
using UILib;
using UILib.Components;
using UILib.Layouts;
using UILib.Patches;
using UnityEngine.SceneManagement;

namespace GraphicsMod {
    [BepInDependency("com.github.Kaden5480.poy-ui-lib")]
    [BepInDependency(
        "com.github.Kaden5480.poy-mod-menu",
        BepInDependency.DependencyFlags.SoftDependency
    )]
    [BepInPlugin("com.github.Kaden5480.poy-graphics-mod", "Graphics Mod", PluginInfo.PLUGIN_VERSION)]
    internal class Plugin : BaseUnityPlugin {
        private static Plugin instance;

        /**
         * <summary>
         * Executes when the plugin is being loaded.
         * </summary>
         */
        private void Awake() {
            instance = this;

            GraphicsMod.Config.Init(this.Config);

            Patcher.PatchAll();

            UIRoot.onInit.AddListener(ShowWarning);

            SceneLoads.AddLoadListener((Scene scene) => {
                Cache.FindObjects(scene);
                Patcher.SceneLoad();
            });

            SceneLoads.AddUnloadListener( delegate {
                Cache.Clear();
            });

            if (AccessTools.AllAssemblies().FirstOrDefault(
                    a => a.GetName().Name == "ModMenu"
                ) != null
            ) {
                Register();
            }
        }

        /**
         * <summary>
         * Handles the initial warning.
         * </summary>
         */
        private void ShowWarning() {
            if (GraphicsMod.Config.shownWarning.Value == true) {
                return;
            }

            Window window = new Window("Graphics Mod", 500f, 300f);
            window.SetContentLayout(LayoutType.Vertical);
            window.SetElementSpacing(20);

            Label header = new Label("!! IMPORTANT !!", 30);
            header.SetSize(400f, 30f);

            Label text = new Label(
                "Graphics mod will override the settings you configured in"
                + " the game's graphics options. Please check"
                + " graphics mod's options (e.g. in mod menu) to ensure they"
                + " are appropriate. This warning will display only once."
            , 25);
            text.SetSize(400f, 150f);

            window.Add(header);
            window.Add(text);

            window.Show();

            GraphicsMod.Config.shownWarning.Value = true;
        }

        /**
         * <summary>
         * Registers with Mod Menu.
         * </summary>
         */
        private void Register() {
            ModInfo info = ModManager.Register(this);
            info.license = "GPL-3.0";
            info.Add(typeof(GraphicsMod.Config));
        }

        /**
         * <summary>
         * Logs a debug message.
         * </summary>
         */
        internal static void LogDebug(string message) {
#if DEBUG
            if (instance == null) {
                Console.WriteLine($"[Debug] GraphicsMod: {message}");
                return;
            }

            instance.Logger.LogInfo(message);
#else
            if (instance != null) {
                instance.Logger.LogDebug(message);
            }
#endif
        }

        /**
         * <summary>
         * Logs an informational message.
         * </summary>
         */
        internal static void LogInfo(string message) {
            if (instance == null) {
                Console.WriteLine($"[Info] GraphicsMod: {message}");
                return;
            }
            instance.Logger.LogInfo(message);
        }

        /**
         * <summary>
         * Logs an error message.
         * </summary>
         */
        internal static void LogError(string message) {
            if (instance == null) {
                Console.WriteLine($"[Error] GraphicsMod: {message}");
                return;
            }
            instance.Logger.LogError(message);
        }
    }
}
