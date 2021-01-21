﻿using System;
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

        private int storedMinutes = 0;
        private int storedHours = 0;
        private int storedDays = 0;

        private float storedTime = 0f;

        internal void Open()
        {
            storedChangeTimeScaleSpeed = TimeControler.main.changeTimeScaleSpeed;

            storedMinutes = TimeControler.realTime.minutes;
            storedDays = TimeControler.realTime.days;
            storedHours = TimeControler.realTime.hours;

            gameIsRunning = true;
        }

        internal void Close()
        {
            gameIsRunning = false;
        }

        internal void ChangeTimeScaleIE(float scale, float speed)
        {
            // PrepperMod.Log("ChangeTimeScaleIE: scale: " + scale.ToString());
            // PrepperMod.Log("ChangeTimeScaleIE: speed: " + speed.ToString());
        }

        internal void BindIncreaseTime()
        {
            if (gameIsRunning == false)
            {
                return;
            }

            PrepperMod.Log("Increase time.");
        }

        internal void BindDecreaseTime()
        {
            if (gameIsRunning == false)
            {
                return;
            }

            PrepperMod.Log("Decrease time.");
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
                TimeControler.main.changeTimeScaleSpeed = 5f;
                TimeControler.main.ChangeTimeScale(0f);

                storedMinutes = TimeControler.realTime.minutes;
                storedDays = TimeControler.realTime.days;
                storedHours = TimeControler.realTime.hours;

                storedTime = Time.time;
            } else
            {
                TimeControler.main.changeTimeScaleSpeed = this.storedChangeTimeScaleSpeed;
                TimeControler.main.ChangeTimeScale(1f);
            }

            PrepperMod.Log("storedMinutes: " + storedMinutes.ToString());
            PrepperMod.Log("storedDays: " + storedDays.ToString());
            PrepperMod.Log("storedHours: " + storedHours.ToString());

            PrepperMod.Log("storedTime: " + storedTime.ToString());
        }

        public static PrepperModController Instance { get; set; } = new PrepperModController();
    }

}