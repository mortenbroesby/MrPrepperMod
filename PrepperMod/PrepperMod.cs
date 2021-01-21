using UnityEngine;
using UModFramework.API;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PrepperMod
{
    [UMFHarmony(4)] //Set this to the number of harmony patches in your mod.
    [UMFScript]
    class PrepperMod : MonoBehaviour
    {
        internal static void Log(string text, bool clean = false)
        {
            using (UMFLog log = new UMFLog()) log.Log(text, clean);
        }

        [UMFConfig]
        public static void LoadConfig()
        {
            PrepperModConfig.Instance.Load();
        }

		void Awake()
		{
			Log("PrepperMod v" + UMFMod.GetModVersion().ToString(), true);
		}

        void Update()
        {
        }
	}
}