using HarmonyLib;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.GameComponents;

namespace FantasyTweaks.Patches
{

    [HarmonyPatch(typeof(DefaultDifficultyModel), "GetPersuasionBonusChance")]
    internal class SilverTongue
    {
        static void Postfix(ref float __result)
        {
            __result = 80f;
        }
    }

    [HarmonyPatch(typeof(LordDefectionCampaignBehavior), "conversation_lord_persuade_option_reaction_pre_reject_on_condition")]
    internal class PersuasionPreRejectPatch
    {
        public static void Postfix(ref bool __result)
        {
            __result = false;
        }
    }
}
