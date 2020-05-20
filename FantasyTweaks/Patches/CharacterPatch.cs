using System;
using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Localization;

namespace FantasyTweaks.Patches
{
    public class CharacterPatch
    {
        private static readonly int _learningLimitMultiplier = 2;
        private static readonly float _learningRateMultiplier = 3.0f;

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

        [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "CalculateLearningLimit", new Type[] 
        {
            typeof(Hero),
            typeof(SkillObject),
            typeof(StatExplainer)
        })]
        public class CalculateLearningLimitHeroPatch
        {
            private static void Postfix(ref int __result)
            {
                __result *= _learningLimitMultiplier;
            }
        }

        [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "CalculateLearningLimit", new Type[] 
        {
            typeof(int),
            typeof(int),
            typeof(TextObject),
            typeof(StatExplainer)
        })]
        public class CalculateLearningPatch
        {
            private static void Postfix(ref int __result)
            {
                __result *= _learningLimitMultiplier;
            }
        }

        [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "CalculateLearningRate", new Type[]
        {
            typeof(Hero),
            typeof(SkillObject),
            typeof(StatExplainer)
        })]
        public class CalculateLearningRateHeroPatch
        {
            private static void Postfix(ref float __result)
            {
                __result *= _learningRateMultiplier;
            }
        }
        
        [HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "CalculateLearningRate", new Type[]
        {
            typeof(int),
            typeof(int),
            typeof(int),
            typeof(int),
            typeof(TextObject),
            typeof(StatExplainer),
        })]
        public class CalculateLearningRatePatch
        {
            private static void Postfix(ref float __result)
            {
                __result *= _learningRateMultiplier;
            }
        }
    }
}
