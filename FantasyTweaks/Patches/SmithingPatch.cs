using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;

namespace FantasyTweaks.Patches
{
    public class SmithingPatch
    {
        private static readonly Double _smithingStaminaMultiplier = 0.25;
        private static readonly int _smithingStaminaHourlyGain = 10;

        [HarmonyPatch(typeof(DefaultSmithingModel), "GetEnergyCostForRefining")]
        public class GetEnergyCostForRefiningPatch
        {
            private static void Postfix(ref int __result)
            {
                __result = (int)(_smithingStaminaMultiplier * __result);
            }
        }

        [HarmonyPatch(typeof(DefaultSmithingModel), "GetEnergyCostForSmithing")]
        public class GetEnergyCostForSmithingPatch
        {
            private static void Postfix(ref int __result)
            {
                __result = (int)(_smithingStaminaMultiplier * __result);
            }
        }

        [HarmonyPatch(typeof(DefaultSmithingModel), "GetEnergyCostForSmelting")]
        public class GetEnergyCostForSmeltingPatch
        {
            private static void Postfix(ref int __result)
            {
                __result = (int)(_smithingStaminaMultiplier * __result);
            }
        }

        [HarmonyPatch(typeof(CraftingCampaignBehavior), "HourlyTick")]
        public class HourlyTickPatch
        {

            private static bool Prefix(CraftingCampaignBehavior __instance)
            {
                Hero hero = PartyBase.MainParty.LeaderHero;

                int heroCraftingStamina = __instance.GetHeroCraftingStamina(hero);
                int maxCraftingStamina = __instance.GetMaxHeroCraftingStamina(hero);
                if (heroCraftingStamina < maxCraftingStamina)
                {
                    int gain = _smithingStaminaHourlyGain;
                    int fromMax = maxCraftingStamina - heroCraftingStamina;
                    if (fromMax < gain)
                    {
                        gain = fromMax;
                    }
                    
                    __instance.SetHeroCraftingStamina(hero, Math.Min(maxCraftingStamina, heroCraftingStamina + gain));

                }

                return false;
            }
        }

    }
}
