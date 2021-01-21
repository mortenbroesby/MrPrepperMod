using UnityEngine;
using HarmonyLib;

namespace PrepperMod.Patches
{
    [HarmonyPatch(typeof(TimeControler))]
    [HarmonyPatch("Awake")]
    static class Patch_TimeControler
    {
        public static void Postfix(TimeControler __instance)
        {
            PrepperModController.Instance.SetTimeController(TimeControler.main);
        }
    }
}