using HarmonyLib;
using System;
using UnityEngine;

namespace DjSkillsManual.DjSkills
{
    //[HarmonyPatch]
    internal class DjVitality
    {
        private static float XPBuffer = 0f;

        [HarmonyPatch(typeof(Player), nameof(Player.SetMaxHealth))]
        [HarmonyPrefix]
        public static void SetMaxHealthPrefix(ref float health, bool flashBar)
        {
            // TODO: Level up base health with food based on duration + temp hp with Rested buff
            if (Stuff.Player != null && Stuff.Player.IsPlayer())
            {
                // TODO: Config shit

                var player = Stuff.Player;
            }
        }
    }
}
