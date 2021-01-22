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

        private int storedMinutes = 0;
        private int storedHours = 0;

        private float storedMinuteFloat = 0f;

        private static int skipAmountInMinutes = 15;

        public float update;

        internal void Open()
        {
            storedMinutes = TimeControler.realTime.minutes;
            storedHours = TimeControler.realTime.hours;
            storedMinuteFloat = TimeControler.main.minuteTime;

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

            PrepperMod.Log("Decrease time.");

            this.SkipBackwards();
        }

        public void SkipForwards()
        {
            TimeControler.DayTime newTime = new TimeControler.DayTime
            {
                minutes = TimeControler.realTime.minutes,
                hours = TimeControler.realTime.hours,
                days = TimeControler.realTime.days
            };

            newTime.minutes = TimeControler.realTime.minutes + skipAmountInMinutes;
            if (newTime.minutes >= 60)
            {
                newTime.hours += newTime.minutes / 60;
                newTime.minutes %= 60;
            }
            if (newTime.hours >= 24)
            {
                newTime.days += newTime.hours / 24;
                newTime.hours %= 24;
            }

            var targetValue = new TimeControler.DayTime
            {
                minutes = newTime.minutes,
                hours = newTime.hours,
                days = newTime.days
            }.ToMinutes();

            PrepperMod.Log("newTime.days: " + newTime.days.ToString());
            PrepperMod.Log("TimeControler.realTime.days: " + TimeControler.realTime.days.ToString());

            if (newTime.days > TimeControler.realTime.days)
            {
                PrepperMod.Log("NO TIME SKIP");
                return;
            }

            PrepperMod.Log("TIME SKIP");

            PrepperMod.Log("targetValue: " + targetValue.ToString());

            TimeControler.realTime.minutes = newTime.minutes;
            TimeControler.realTime.hours = newTime.hours;
        }

        public void SkipBackwards()
        {
            TimeControler.DayTime newTime = new TimeControler.DayTime
            {
                minutes = TimeControler.realTime.minutes,
                hours = TimeControler.realTime.hours,
                days = TimeControler.realTime.days
            };

            newTime.minutes = TimeControler.realTime.minutes - skipAmountInMinutes;
            if (newTime.minutes < 0)
            {
                newTime.hours--;
                newTime.minutes += 60;
            }
            if (newTime.hours < 0)
            {
                newTime.days--;
                newTime.hours += 24;
            }

            var targetValue = new TimeControler.DayTime
            {
                minutes = newTime.minutes,
                hours = newTime.hours,
                days = newTime.days
            }.ToMinutes();

            PrepperMod.Log("newTime.days: " + newTime.days.ToString());
            PrepperMod.Log("TimeControler.realTime.days: " + TimeControler.realTime.days.ToString());

            if (newTime.days < TimeControler.realTime.days)
            {
                PrepperMod.Log("NO TIME SKIP");
                return;
            }

            PrepperMod.Log("TIME SKIP");

            PrepperMod.Log("targetValue: " + targetValue.ToString());

            TimeControler.realTime.minutes = newTime.minutes;
            TimeControler.realTime.hours = newTime.hours;
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

        internal void UpdateTimeStop()
        {
            PrepperMod.Log("Toggle time-stop. Time is: " + (timeIsStopped ? "STOPPED" : "STARTED"));

            if (timeIsStopped)
            {
                storedMinutes = TimeControler.realTime.minutes;
                storedHours = TimeControler.realTime.hours;
                storedMinuteFloat = TimeControler.main.minuteTime;
            } else
            {
                storedMinutes = 0;
                storedHours = 0;
                storedMinuteFloat = 0f;
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
                TimeControler.realTime.minutes = storedMinutes;
                TimeControler.realTime.hours = storedHours;
                TimeControler.main.minuteTime = storedMinuteFloat;
            }

        }

        public static PrepperModController Instance { get; set; } = new PrepperModController();
    }

}