using HarmonyLib;
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
}
