using HarmonyLib;
using System;
using UnityEngine;

namespace DjSkillsManual.DjSkills
{

    [HarmonyPatch(typeof(Player), nameof(Player.UpdateStats))]
    public class DjStrength
    {
        private static float XPBuffer = 0f;

        public static void Postfix()
        {
            if (Stuff.Player != null && Stuff.Player.IsPlayer())
            {
                // TODO: Config shit

                var player = Stuff.Player;
                var walk = Mathf.Floor(player.GetVelocity().magnitude) > 1;
                var run = player.IsRunning();

                // TODO: Increase when: Crafting shit, walking with more than 75% weight, running with more than 75%

                bool IsOverThreshold = player.GetInventory().GetTotalWeight() > (player.GetMaxCarryWeight() * 0.75); // TODO: Configurable threshold
                //DjSkillsMod.DjLogger.LogInfo($"Walk: {walk} | Run: {run} | Threshold: {IsOverThreshold} | Ship: {player.GetStandingOnShip() != null}");
                if ((walk || run) && IsOverThreshold && player.GetStandingOnShip() == null)
                {
                    //DjSkillsMod.DjLogger.LogInfo($"Shit it happening! ID: {(int)DjSkillsMod.StrengthSkill} | Weight: {player.GetInventory().GetTotalWeight()}");
                    XPBuffer += (player.GetInventory().GetTotalWeight() / (run ? 400000 : 600000)) * 1f;
                }

                if (XPBuffer > 0.1f)
                {
                    player.RaiseSkill(DjSkillsMod.StrengthSkill, (float)Math.Round(XPBuffer, 2));
                    XPBuffer = 0f;
                }
            }
        }
    }

    [HarmonyPatch(typeof(Player), nameof(Player.GetMaxCarryWeight))]
    public class DjGetWeight
    {
        public static void Postfix(ref Player __instance, ref float __result)
        {
            var level = __instance.GetSkillFactor(DjSkillsMod.StrengthSkill); // Level / 100
            __result += level * 1000; // TODO: Config
        }
    }
}
