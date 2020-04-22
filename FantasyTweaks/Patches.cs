using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using SandBox.TournamentMissions.Missions;

namespace FantasyTweaks
{
    internal class Patches
    {
        private static readonly Double SMITHING_STAMINA_MULTIPLIER = 0.25;

        [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "get_LevelsPerAttributePoint")]
        public class GetLevelsPerAttributePointPatch
        {
            private static bool Prefix(ref int __result)
            {
                __result = 1;
                return false;
            }
        }
        
        [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "get_FocusPointsPerLevel")]
        public class GetFocusPointsPerLevelPatch
        { 
            private static bool Prefix(ref int __result)
            {
                __result = 2;
                return false;
            }
        }

        [HarmonyPatch(typeof(DefaultSmithingModel), "GetEnergyCostForRefining")]
        public class GetEnergyCostForRefiningPatch
        {
            private static void Postfix(ref int __result)
            {
                __result = (int) (SMITHING_STAMINA_MULTIPLIER * __result);
            }
        }

        [HarmonyPatch(typeof(DefaultSmithingModel), "GetEnergyCostForSmithing")]
        public class GetEnergyCostForSmithingPatch
        {
            private static void Postfix(ref int __result)
            {
                __result = (int) (SMITHING_STAMINA_MULTIPLIER * __result);
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

        [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
        public class GetRenownRewardPatch
        {
            private static void Postfix(ref int __result)
            {
                __result = __result * 2;
            }
        }

        [HarmonyPatch(typeof(TournamentBehavior), "get_OverallExpectedDenars")]
        public class GetOverallExpectedDenarsPatch
        {
            private static void Postfix(ref int __result)
            {
                __result = __result + 1000;
            }
        }

    }

}
