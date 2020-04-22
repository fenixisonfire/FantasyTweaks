using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

namespace FantasyTweaks.Patches
{
    public class SmithingPatch
    {
        private static readonly Double SMITHING_STAMINA_MULTIPLIER = 0.25;

        [HarmonyPatch(typeof(DefaultSmithingModel), "GetEnergyCostForRefining")]
        public class GetEnergyCostForRefiningPatch
        {
            private static void Postfix(ref int __result)
            {
                __result = (int)(SMITHING_STAMINA_MULTIPLIER * __result);
            }
        }

        [HarmonyPatch(typeof(DefaultSmithingModel), "GetEnergyCostForSmithing")]
        public class GetEnergyCostForSmithingPatch
        {
            private static void Postfix(ref int __result)
            {
                __result = (int)(SMITHING_STAMINA_MULTIPLIER * __result);
            }
        }

        [HarmonyPatch(typeof(DefaultSmithingModel), "GetEnergyCostForSmelting")]
        public class GetEnergyCostForSmeltingPatch
        {
            private static void Postfix(ref int __result)
            {
                __result = (int)(SMITHING_STAMINA_MULTIPLIER * __result);
            }
        }
    }
}
