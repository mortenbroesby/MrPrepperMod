using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Collections;
using UnityEngine;
using UModFramework.API;
using HarmonyLib;

namespace PrepperMod
{
    public class PrepperModController : MonoBehaviour
    {
        private TimeControler timeController;

        private bool gameIsRunning = false;
        private bool isTimeStopped = false;

        internal void SetTimeController(TimeControler __instance)
        {
            gameIsRunning = true;
            timeController = __instance;
        }

        internal void Close()
        {
            gameIsRunning = false;
            timeController = null;
        }

        internal void BindIncreaseTime()
        {
            if (!this.gameIsRunning)
            {
                return;
            }

            PrepperMod.Log("DEBUG: Increase time.");
        }

        internal void BindDecreaseTime()
        {
            if (!this.gameIsRunning)
            {
                return;
            }

            PrepperMod.Log("DEBUG: Decrease time.");
        }

        internal void BindToggleTimeStop()
        {
            if (!this.gameIsRunning)
            {
                return;
            }

            isTimeStopped = !isTimeStopped;

            this.UpdateTimeStop();
        }

        public void UpdateTimeStop()
        {

            PrepperMod.Log("DEBUG: Toggle time-stop. Status: " + (isTimeStopped ? "on" : "off"));
            UMFGUI.AddConsoleText("Successfully turned Time " + (isTimeStopped ? "on" : "off") + ".");
        }

        public static PrepperModController Instance { get; set; } = new PrepperModController();
    }

}