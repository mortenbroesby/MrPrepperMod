using UnityEngine;
using HarmonyLib;

namespace PrepperMod.Patches
{
    [HarmonyPatch(typeof(TimeControler))]
    [HarmonyPatch("ChangeTimeScaleIE")]
    static class Patch_ChangeTimeScaleIE
    {
        public static void Prefix(float scale, float speed)
        {
            PrepperModController.Instance.ChangeTimeScaleIE(scale, speed);
        }
    }
}