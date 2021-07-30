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

    [HarmonyPatch(typeof(CraftingCampaignBehavior), "HourlyTick")]
    public class HourlyTickPatch
    {
        static bool Prefix(CraftingCampaignBehavior __instance)
        {
            Hero hero = PartyBase.MainParty.LeaderHero;

            int heroCraftingStamina = __instance.GetHeroCraftingStamina(hero);
            int maxCraftingStamina = __instance.GetMaxHeroCraftingStamina(hero);
            int gain = 50;

            int newStamina = Math.Min(maxCraftingStamina, heroCraftingStamina + gain);

            __instance.SetHeroCraftingStamina(hero, newStamina);

            return true;
        }
    }
}
