using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace FantasyTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultSmithingModel))]
    class DefaultSmithingModelPatch
    {
        private static readonly Double SMITHING_STAMINA_MULTIPLIER = 0.25;
        private static readonly float RESEARCH_RATE_MULTIPLIER = 10f;
        private static readonly int RESEARCH_GAIN_MULTIPLIER = 2;

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

        [HarmonyPostfix]
        [HarmonyPatch("ResearchPointsNeedForNewPart")]
        static void ResearchPointsNeedForNewPartPatch(int totalPartCount, int openedPartCount, ref float __result)
        {
            __result = Math.Max(1f, __result / RESEARCH_RATE_MULTIPLIER);
        }

        [HarmonyPostfix]
        [HarmonyPatch("GetPartResearchGainForSmeltingItem")]
        static void GetPartResearchGainForSmeltingItemPatch(ItemObject item, Hero hero, ref int __result)
        {
            __result *= RESEARCH_GAIN_MULTIPLIER;
        }

        [HarmonyPostfix]
        [HarmonyPatch("GetPartResearchGainForSmithingItem")]
        static void GetPartResearchGainForSmithingItemPatch(ItemObject item, Hero hero, ref int __result)
        {
            __result *= RESEARCH_GAIN_MULTIPLIER;
        }
    }

    [HarmonyPatch(typeof(CraftingCampaignBehavior), "HourlyTick")]
    public class HourlyTickPatch
    {
        private static readonly int SMITHING_STAMINA_GAIN = 50;

        static bool Prefix(CraftingCampaignBehavior __instance)
        {
            Hero hero = PartyBase.MainParty.LeaderHero;

            if (hero != null)
            {
                int heroCraftingStamina = __instance.GetHeroCraftingStamina(hero);
                int maxCraftingStamina = __instance.GetMaxHeroCraftingStamina(hero);

                int newStamina = Math.Min(maxCraftingStamina, heroCraftingStamina + SMITHING_STAMINA_GAIN);

                __instance.SetHeroCraftingStamina(hero, newStamina);
            }
            return true;
        }
    }
}
