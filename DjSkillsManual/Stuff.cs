using HarmonyLib;

namespace DjSkillsManual
{
    #region
    public class Stuff
    {
        public static Player Player;


        [HarmonyPatch(typeof(Player), nameof(Player.UpdateStats))]
        public static class SI_Player
        {
            public static void Postfix(ref Player __instance)
            {
                if (__instance != null)
                    Player = __instance;
            }
        }
    }
    #endregion

    #region THE WORKING SHIT
    //[HarmonyPatch(typeof(Player), nameof(Player.OnDeath))]
    public static class MyClass
    {
        private static void Postfix(Player __instance)
        {
            DjSkillsMod.DjLogger.LogWarning("Hello there, I work now");
        }
    }
    #endregion

}
