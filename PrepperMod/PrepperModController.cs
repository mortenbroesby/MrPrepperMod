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
        #region Variables
        public bool gameIsRunning = false;
        public bool timeIsStopped = false;

        private int skipDelta = 15;

        private TimeControler.DayTime storedTime = new TimeControler.DayTime
        {
            minutes = 0,
            hours = 12,
            days = 0
        };
        #endregion

        #region Public Functions
        public void Open()
        {
            storedTime = TimeControler.realTime;
            gameIsRunning = true;
        }

        public void Close()
        {
            gameIsRunning = false;
        }

        public void SetSkipDelta(int delta)
        {
            this.skipDelta = delta;
        }
        public void OnUpdate()
        {
            if (gameIsRunning && timeIsStopped)
            {
                TimeControler.realTime = storedTime;
            }
        }
        #endregion

        #region Keybind Functions
        internal void BindIncreaseTime()
        {
            if (gameIsRunning == false)
            {
                return;
            }

            PrepperMod.Log("Increase time.");

            this.SkipForwards();
        }

        internal void BindDecreaseTime()
        {
            if (gameIsRunning == false)
            {
                return;
            }

            this.SkipBackwards();
        }
        internal void BindToggleTimeStop()
        {
            if (gameIsRunning)
            {
                this.ToggleAndStoreTime();
            }
        }
        #endregion

        #region Private Functions
        private void SkipForwards()
        {
            PrepperMod.Log("Skip Forwards.");
            SkipTimeInMinutes(skipDelta);
        }

        private void SkipBackwards()
        {
            PrepperMod.Log("Skip Backwards.");
            SkipTimeInMinutes(-skipDelta);
        }

        private void SkipTimeInMinutes(int deltaTime)
        {
            TimeControler.DayTime newTime = TimeControler.realTime;

            newTime.minutes = TimeControler.realTime.minutes + deltaTime;
            if (newTime.minutes < 0)
            {
                newTime.hours--;
                newTime.minutes += 60;
            }
            else if (newTime.minutes >= 60)
            {
                newTime.hours += newTime.minutes / 60;
                newTime.minutes %= 60;
            }

            if (newTime.hours < 0)
            {
                newTime.days--;
                newTime.hours += 24;
            } else if (newTime.hours >= 24)
            {
                newTime.days += newTime.hours / 24;
                newTime.hours %= 24;
            }

            // Do not skip between days for now...
            if (newTime.days < TimeControler.realTime.days || newTime.days > TimeControler.realTime.days)
            {
                return;
            }

            PrepperMod.Log("Skipping to time: " + newTime.ToLongString());

            storedTime = newTime;

            TimeControler.realTime = newTime;
        }
        private void ToggleAndStoreTime()
        {
            timeIsStopped = !timeIsStopped;

            PrepperMod.Log("Toggle time-stop. Time is: " + (timeIsStopped ? "STOPPED" : "STARTED"));

            if (timeIsStopped)
            {
                storedTime = TimeControler.realTime;
            }
        }
        #endregion

        public static PrepperModController Instance { get; set; } = new PrepperModController();
    }

}