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

        private int skipDelta = 15;

        private TimeControler.DayTime storedTime = new TimeControler.DayTime
        {
            minutes = 0,
            hours = 12,
            days = 0
        };

        public float update;

        internal void Open()
        {
            storedTime = TimeControler.realTime;
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

        public void SetSkipDelta(int delta)
        {
            this.skipDelta = delta;
        }

        public void SkipForwards()
        {
            PrepperMod.Log("Skip Forwards.");
            SkipTimeInMinutes(skipDelta);
        }

        public void SkipBackwards()
        {
            PrepperMod.Log("Skip Backwards.");
            SkipTimeInMinutes(-skipDelta);
        }

        public void SkipTimeInMinutes(int deltaTime)
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

            TimeControler.realTime = newTime;
        }


        internal void BindToggleTimeStop()
        {
            if (gameIsRunning == false)
            {
                return;
            }

            timeIsStopped = !timeIsStopped;

            this.StoreTime();
        }

        internal void StoreTime()
        {
            PrepperMod.Log("Toggle time-stop. Time is: " + (timeIsStopped ? "STOPPED" : "STARTED"));

            if (timeIsStopped)
            {
                storedTime = TimeControler.realTime;
            }
        }

        public void OnUpdate()
        {
            if (gameIsRunning == false)
            {
                return;
            }

            var shouldUpdateTime = false;
            update += Time.deltaTime;

            if (update > 0.5f)
            {
                shouldUpdateTime = true;
                update = 0.0f;     
            }

            if (timeIsStopped && shouldUpdateTime)
            {
                TimeControler.realTime = storedTime;
            }

        }

        public static PrepperModController Instance { get; set; } = new PrepperModController();
    }

}