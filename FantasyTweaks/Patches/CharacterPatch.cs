using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace FantasyTweaks.Patches
{
    internal class CharacterPatch
    {
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
    }
}
