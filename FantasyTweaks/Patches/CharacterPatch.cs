using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;

namespace FantasyTweaks.Patches
{

    [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel))]
    public class DefaultCharacterDevelopmentModelPatch
    {
        private static readonly float LEARNING_RATE_LIMIT_MULTIPLIER = 1.005f;

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
        static void CalculateLearningLimitPostfix(ref ExplainedNumber __result)
        {
            float newLearningLimit = __result.ResultNumber * LEARNING_RATE_LIMIT_MULTIPLIER - __result.ResultNumber;
            __result.AddFactor(newLearningLimit);
            __result.LimitMax(500);
        }
    }
}
