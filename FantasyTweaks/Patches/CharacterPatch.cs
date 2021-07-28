using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace FantasyTweaks.Patches
{

    [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel))]
    public class DefaultCharacterDevelopmentModelPatch
    {
        private static readonly int LEARNING_RATE_LIMIT_MULTIPLIER = 2;

        [HarmonyPrefix]
        [HarmonyPatch("get_LevelsPerAttributePoint")]
        static bool LevelsPerAttributePointPrefix(ref int __result)
        {
            __result = 1;
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("get_FocusPointsPerLevel")]
        static bool FocusPointsPerLevelPrefix(ref int __result)
        {
            __result = 2;
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch("CalculateLearningLimit")]
        static void CalculateLearningLimitPostfix(ref int __result)
        {
            __result *= LEARNING_RATE_LIMIT_MULTIPLIER;
        }
    }
}
