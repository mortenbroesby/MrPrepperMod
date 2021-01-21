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

    [HarmonyPatch(typeof(TimeControler))]
    [HarmonyPatch("Stop")]
    static class Patch_Stop
    {
        public static bool Prefix()
        {
            var timeIsStopped = PrepperModController.Instance.timeIsStopped;
            PrepperMod.Log("timeIsStopped: " + timeIsStopped.ToString());

            return timeIsStopped == false;
        }
    }
}