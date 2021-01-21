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
        private bool gameIsRunning = false;
        private bool timeIsStopped = false;

        internal void Open()
        {
            this.gameIsRunning = true;
        }

        internal void Close()
        {
            this.gameIsRunning = false;
        }

        internal void BindIncreaseTime()
        {
            if (gameIsRunning == false)
            {
                return;
            }

            PrepperMod.Log("DEBUG: Increase time.");
        }

        internal void BindDecreaseTime()
        {
            if (gameIsRunning == false)
            {
                return;
            }

            PrepperMod.Log("DEBUG: Decrease time.");
        }

        internal void BindToggleTimeStop()
        {
            if (gameIsRunning == false)
            {
                return;
            }

            timeIsStopped = !timeIsStopped;
            this.UpdateTimeStop();
        }

        public void UpdateTimeStop()
        {
            PrepperMod.Log("Toggle time-stop. Time is: " + (timeIsStopped ? "STOPPED" : "STARTED"));

            if (timeIsStopped)
            {
                TimeControler.main.ChangeTimeScale(0f);
            } else
            {
                TimeControler.main.ChangeTimeScale(1f);
            }
        }

        public static PrepperModController Instance { get; set; } = new PrepperModController();
    }

}