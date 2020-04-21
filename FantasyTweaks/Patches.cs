using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace FantasyTweaks
{
    internal class Patches
    {

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

    }

}
