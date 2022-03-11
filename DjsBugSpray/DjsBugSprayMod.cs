using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace DjsBugSpray
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("valheim.exe")]
    public class DjsBugSprayMod : BaseUnityPlugin
    {
        internal static ManualLogSource DjLogger;
        private readonly Harmony _harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        public DjsBugSprayMod()
        {
            DjLogger = Logger;
        }

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            _harmony.PatchAll();
            Logger.LogInfo("All patched up!");
        }

        private void OnDestroy()
        {
            _harmony?.UnpatchSelf();
            Logger.LogInfo("Cleaning up my shit!");
        }
    }

    [HarmonyPatch]
    partial class Test
    {
        [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.StartAttack))]
        [HarmonyPrefix]
        public static bool StartAttackPrefixPatch(ref Humanoid __instance, Character target, bool secondaryAttack)
        {
            return target == null || !target.IsPlayer() ? true : !__instance.name.Contains("Deathsquito");
        }
    }

}
