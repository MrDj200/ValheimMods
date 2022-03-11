using Jotunn.Configs;
using Jotunn.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjSkillsManual
{
    internal class DjUtils
    {
        /// <summary>
        /// Registers a skill with Jotunn
        /// </summary>
        /// <param name="name">Name of the skill</param>
        /// <param name="description">Description of the Skill</param>
        /// <returns></returns>
        public static Skills.SkillType DjAddSkill(string name, string description)
        {
            string ident = $"{PluginInfo.PLUGIN_GUID}{name}Skill";
            var skillID = SkillManager.Instance.GetSkill(ident);

            Skills.SkillType skill = 0;

            if (skillID == null)
            {
                DjSkillsMod.DjLogger.LogInfo($"Added {name} Skill!");
                SkillConfig cfg = new SkillConfig()
                {
                    Name = name,
                    Description = description,
                    Identifier = ident,
                    IncreaseStep = 1f,
                };
                skill = SkillManager.Instance.AddSkill(cfg);
            }
            else
            {
                skill = skillID.m_skill;
                DjSkillsMod.DjLogger.LogInfo($"Skipped adding {name} Skill, because it already exists!");
            }

            return skill;
        }
    }
}
