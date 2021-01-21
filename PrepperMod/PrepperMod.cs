using UnityEngine;
using UModFramework.API;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PrepperMod
{
    [UMFHarmony(3)] //Set this to the number of harmony patches in your mod.
    [UMFScript]
    class PrepperMod : MonoBehaviour
    {
        private float update;

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
            update += Time.deltaTime;

            if (update > 1.0f)
            {
                update = 0.0f;

                Log("Update time: " + update.ToString());
            }
        }
	}
}