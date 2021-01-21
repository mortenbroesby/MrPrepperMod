using System;
using UModFramework.API;

namespace PrepperMod
{
    public class PrepperModConfig
    {
        private static readonly string configVersion = "1.1";

        public static float ModdedTimeScale = 0f;

        private static string[] KeysIncTime;
        private static string[] KeysDecTime;
        private static string[] KeysStopTime;

        internal void Load()
        {
            PrepperMod.Log("Loading settings.");

            try
            {
                using (UMFConfig config = new UMFConfig())
                {
                    string configVersion = config.Read("ConfigVersion", new UMFConfigString());
                    if (configVersion != string.Empty && configVersion != PrepperModConfig.configVersion)
                    {
                        config.DeleteConfig(false);
                        PrepperMod.Log("The config file was outdated and has been deleted. A new config will be generated.");
                    }

                    //cfg.Write("SupportsHotLoading", new UMFConfigBool(false)); //Uncomment if your mod can't be loaded once the game has started.
                    config.Write("ModDependencies", new UMFConfigStringArray(new string[] { "" })); //A comma separated list of mod/library names that this mod requires to function. Format: SomeMod:1.50,SomeLibrary:0.60
                    config.Read("LoadPriority", new UMFConfigString("Normal"));
                    config.Write("MinVersion", new UMFConfigString("0.53.0"));
                    config.Write("MaxVersion", new UMFConfigString("0.54.99999.99999")); //This will prevent the mod from being loaded after the next major UMF release
                    config.Write("UpdateURL", new UMFConfigString(""));
                    config.Write("ConfigVersion", new UMFConfigString(PrepperModConfig.configVersion));

                    PrepperMod.Log("Finished UMF Settings.");

                    // ModdedTimeScale = config.Read("Modded timeScale", new UMFConfigFloat(0f, 0f, 3f), "The Modded Time-scale In-game.");

                    KeysIncTime = config.Read("Increment Time", new UMFConfigStringArray(new string[0], true), "The key(s) used to increase time.");
                    KeysDecTime = config.Read("Decrement Time", new UMFConfigStringArray(new string[0], true), "The key(s) used to decrease time.");
                    KeysStopTime = config.Read("Toggle Time-Stop", new UMFConfigStringArray(new string[0], true), "The key(s) used to toggle stopped time.");

                    UpdateBinds();

                    PrepperMod.Log("Finished loading settings.");
                }
            }
            catch (Exception e)
            {
                PrepperMod.Log("Error loading mod settings: " + e.Message + "(" + e.InnerException?.Message + ")");
            }
        }

        internal void UpdateBinds()
        {
            for (int i = 0; i < KeysIncTime.Length; i++) UMFGUI.RegisterBind("KeysIncTime" + i.ToString(), KeysIncTime[i], PrepperModController.Instance.BindIncreaseTime);
            for (int i = 0; i < KeysDecTime.Length; i++) UMFGUI.RegisterBind("KeysDecTime" + i.ToString(), KeysDecTime[i], PrepperModController.Instance.BindDecreaseTime);
            for (int i = 0; i < KeysStopTime.Length; i++) UMFGUI.RegisterBind("KeysStopTime" + i.ToString(), KeysStopTime[i], PrepperModController.Instance.BindToggleTimeStop);
        }

        public static PrepperModConfig Instance { get; } = new PrepperModConfig();
    }
}