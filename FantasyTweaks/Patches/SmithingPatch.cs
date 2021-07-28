using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;

namespace FantasyTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultSmithingModel))]
    class DefaultSmithingModelPatch
    {
        private static readonly Double SMITHING_STAMINA_MULTIPLIER = 0.25;

        [HarmonyPostfix]
        [HarmonyPatch("GetEnergyCostForRefining")]
        static void GetEnergyCostForRefiningPostfix(ref int __result)
        {
            __result = (int)(SMITHING_STAMINA_MULTIPLIER * __result);
        }

        [HarmonyPostfix]
        [HarmonyPatch("GetEnergyCostForSmithing")]
        static void GetEnergyCostForSmithingPostfix(ref int __result)
        {
            __result = (int)(SMITHING_STAMINA_MULTIPLIER * __result);
        }


        [HarmonyPostfix]
        [HarmonyPatch("GetEnergyCostForSmelting")]
        static void GetEnergyCostForSmeltingPostfix(ref int __result)
        {
            __result = (int)(SMITHING_STAMINA_MULTIPLIER * __result);
        }
    }

    [HarmonyPatch(typeof(CraftingCampaignBehavior))]
    class CraftingCampaignBehaviorPatch
    {
        private static readonly int SMITHING_STAMINA_HOURLY_GAIN = 5;

        [HarmonyPrefix]
        [HarmonyPatch("HourlyTick")]
        static bool HourlyTickPrefix(CraftingCampaignBehavior __instance)
        {
            Hero hero = PartyBase.MainParty.LeaderHero;

            int heroCraftingStamina = __instance.GetHeroCraftingStamina(hero);
            int maxCraftingStamina = __instance.GetMaxHeroCraftingStamina(hero);
            if (heroCraftingStamina < maxCraftingStamina)
            {
                int gain = SMITHING_STAMINA_HOURLY_GAIN;
                int fromMax = maxCraftingStamina - heroCraftingStamina;
                if (fromMax < gain)
                {
                    gain = fromMax;
                }

                __instance.SetHeroCraftingStamina(hero, Math.Min(maxCraftingStamina, heroCraftingStamina + gain));

            }

            return true;
        }
    }
}
