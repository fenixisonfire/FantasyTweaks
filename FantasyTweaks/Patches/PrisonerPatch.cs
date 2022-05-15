using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;

namespace FantasyTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPrisonerRecruitmentCalculationModel))]
    class DefaultPrisonerRecruitmentCalculationModelPatch
    {
        private static readonly float CONFORMITY_MULTIPLIER = 20.0f;

        [HarmonyPostfix]
        [HarmonyPatch("GetConformityChangePerHour")]
        static void GetConformityChangePerHourPostfix(PartyBase party, CharacterObject troopToBoost, ref int __result)
        {
            if (party.LeaderHero == Hero.MainHero)
            {
                __result = MathF.Round(__result * CONFORMITY_MULTIPLIER);
            }
        }
    }
}
