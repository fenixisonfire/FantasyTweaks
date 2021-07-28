using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;

namespace FantasyTweaks.Patches
{
    
    [HarmonyPatch(typeof(Hero))]
    class HeroPatch
    {
        private static readonly float SKILL_XP_MULTIPLIER = 5.0f;

        [HarmonyPrefix]
        [HarmonyPatch("AddSkillXp")]
        static void AddSkillXpPrefix(Hero __instance, SkillObject skill, ref float xpAmount)
        {
            if (__instance != null && skill != null && __instance.HeroDeveloper != null && skill.GetName() != null && Hero.MainHero != null)
            {
                xpAmount *= SKILL_XP_MULTIPLIER;
            }
        }
    }
}
