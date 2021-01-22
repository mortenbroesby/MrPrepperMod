using UnityEngine;
using HarmonyLib;

namespace PrepperMod.Patches
{
    [HarmonyPatch(typeof(LoadingScreen))]
    [HarmonyPatch("GameLoaded")]
    static class Patch_GameLoaded
    {
        public static void Postfix()
        {
            PrepperMod.Log("Game Loaded");
            PrepperModController.Instance.GameIsRunning(true);
        }
    }

    [HarmonyPatch(typeof(LoadingScreen))]
    [HarmonyPatch("LoadMenu")]
    static class Patch_LoadMenu
    {
        public static void Postfix()
        {
            PrepperMod.Log("Menu Loaded");
            PrepperModController.Instance.GameIsRunning(false);
        }
    }
}