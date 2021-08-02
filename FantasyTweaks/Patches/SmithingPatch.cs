using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

namespace FantasyTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultSmithingModel))]
    class DefaultSmithingModelPatch
    {
        private static readonly Double SMITHING_STAMINA_MULTIPLIER = 0.25;
        private static readonly int RESEARCH_RATE_MULTIPLIER = 50;

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
        static void ResearchPointsNeedForNewPartPatch(int count, ref int __result)
        {
            __result = __result / RESEARCH_RATE_MULTIPLIER;
        }
    }

    [HarmonyPatch(typeof(CraftingCampaignBehavior), "HourlyTick")]
    public class HourlyTickPatch
    {
        private static readonly int SMITHING_STAMINA_GAIN = 50;

        static bool Prefix(CraftingCampaignBehavior __instance)
        {
            Hero hero = PartyBase.MainParty.LeaderHero;

            int heroCraftingStamina = __instance.GetHeroCraftingStamina(hero);
            int maxCraftingStamina = __instance.GetMaxHeroCraftingStamina(hero);

            int newStamina = Math.Min(maxCraftingStamina, heroCraftingStamina + SMITHING_STAMINA_GAIN);

            __instance.SetHeroCraftingStamina(hero, newStamina);

            return true;
        }
    }
}
