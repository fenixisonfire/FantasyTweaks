using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;

namespace FantasyTweaks.Patches
{
    class HeroPatch
    {
        private static readonly float SKILL_XP_MULTIPLIER = 5.0f;

        [HarmonyPatch(typeof(Hero))]
        public class Patches
        {
            [HarmonyPrefix]
            [HarmonyPatch("AddSkillXp")]
            public static void Prefix(Hero __instance, SkillObject skill, ref float xpAmount)
            {
                if (shouldApplySkillMultiplier(__instance, skill))
                {
                    xpAmount *= SKILL_XP_MULTIPLIER;
                }
            }
            private static bool shouldApplySkillMultiplier(Hero __instance, SkillObject skill)
            {
                return __instance != null && skill != null && __instance.HeroDeveloper != null && skill.GetName() != null && Hero.MainHero != null;
            }
        }
    }
}
