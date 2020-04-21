using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

namespace FantasyTweaks
{
    internal class Patches
    {
        private static readonly Double SMITHING_STAMINA_MULTIPLIER = 0.25;

        [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "get_LevelsPerAttributePoint")]
        public class GetLevelsPerAttributePointPatch
        {
            private static void Postfix(ref int __result)
            {
                __result = 1;
            }
        }
        
        [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "get_FocusPointsPerLevel")]
        public class GetFocusPointsPerLevelPatch
        { 
            private static void Postfix(ref int __result)
            {
                __result = 2;
            }
        }

        [HarmonyPatch(typeof(DefaultSmithingModel), "GetEnergyCostForRefining")]
        public class GetEnergyCostForRefining
        {
            private static void Postfix(ref int __result)
            {
                __result = (int) (SMITHING_STAMINA_MULTIPLIER * __result);
            }
        }

        [HarmonyPatch(typeof(DefaultSmithingModel), "GetEnergyCostForSmithing")]
        public class GetEnergyCostForSmithing
        {
            private static void Postfix(ref int __result)
            {
                __result = (int) (SMITHING_STAMINA_MULTIPLIER * __result);
            }
        }

        [HarmonyPatch(typeof(DefaultSmithingModel), "GetEnergyCostForSmelting")]
        public class GetEnergyCostForSmelting
        {
            private static void Postfix(ref int __result)
            {
                __result = (int) (SMITHING_STAMINA_MULTIPLIER * __result);
            }
        }
    }

}
