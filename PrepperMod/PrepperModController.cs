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
        public bool gameIsRunning = false;

        public bool timeIsStopped = false;

        private float storedChangeTimeScaleSpeed = 3f;

        internal void Open()
        {
            storedChangeTimeScaleSpeed = TimeControler.main.changeTimeScaleSpeed;
            gameIsRunning = true;
        }

        internal void Close()
        {
            gameIsRunning = false;
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

            UpdateTimeStop();
        }

        public void UpdateTimeStop()
        {
            if (timeIsStopped)
            {
                PrepperMod.Log("Toggle time-stop. Status: Time is STOPPED!");

                TimeControler.main.targetTimeScale = 0f;
                TimeControler.main.timeScaleOnUnblock = 0f;
                TimeControler.main.changeTimeScaleSpeed = 0f;

                TimeControler.main.ChangeTimeScale(0f);
            } else
            {
                PrepperMod.Log("Toggle time-stop. Status: Time is STARTED!");

                TimeControler.main.changeTimeScaleSpeed = storedChangeTimeScaleSpeed;
                TimeControler.main.targetTimeScale = 1f;
                TimeControler.main.timeScaleOnUnblock = 1f;

                TimeControler.main.ChangeTimeScale(1f);
            }
        }

        public static PrepperModController Instance { get; set; } = new PrepperModController();
    }

}