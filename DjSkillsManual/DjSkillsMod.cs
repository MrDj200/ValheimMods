using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace DjSkillsManual
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("valheim.exe")]
    [BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
    public class DjSkillsMod : BaseUnityPlugin
    {
        internal static ManualLogSource DjLogger;

        private readonly Harmony _harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        public static Skills.SkillType StrengthSkill { get; private set; }
        public static Skills.SkillType VitalitySkill { get; private set; }

        public DjSkillsMod()
        {
            DjLogger = Logger;
        }

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            AddStuff();
        }

        private void AddStuff()
        {
            StrengthSkill = DjUtils.DjAddSkill("Strength", "Increases your Max Carry weight");
            VitalitySkill = DjUtils.DjAddSkill("Vitality", "Increases your Max Health");

            _harmony?.PatchAll();
            Logger.LogInfo("All patched up!");
        }

        private void OnDestroy()
        {
            // TODO: Figure out a way to remove skills
            _harmony?.UnpatchSelf();
            Logger.LogInfo("Cleaning up my shit!");
        }
    }
}
