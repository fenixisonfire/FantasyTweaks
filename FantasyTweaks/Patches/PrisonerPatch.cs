using System;
using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace FantasyTweaks.Patches
{
    public class PrisonerPatch
    {
        // Not needed as DailyTickPatch below is pathing all prisoners to be recruitable after 1 tick
        // [HarmonyPatch(typeof(DefaultPrisonerRecruitmentCalculationModel), "GetDailyRecruitedPrisoners")]
        public class GetDailyRecruitedPrisonersPatch
        {
            /*
             * RECRUITMENT_PROBABILITY represents the likelihood that a prisoner is to become recruitable per (daily) tick.
             * p > 1 means guaranteed 1 will become recuitable from one stack and (p - 1)% for second the second stack of agents to become recruitable if it exists
             * p == 1 means guaranteed 1 will become recruitable
             * p < 1 means p% chance that 1 will become recruitable
             * 
             * The position of the value in the array represents the tier of unit it's for, i.e 0th element for tier 0, 1st element for tier 1, etc.
            */
            private static readonly float[] _recruitmentProbability = new float[7] { 1f, 0.9f, 0.8f, 0.7f, 0.6f, 0.5f, 0.4f };

            public static bool Prefix(ref float[] __result, MobileParty mainParty)
            {
                if (mainParty.IsMainParty)
                {
                    __result = _recruitmentProbability;
                    return false;
                }

                return true;
            }
        }

        [HarmonyPatch(typeof(DefaultPrisonerRecruitmentCalculationModel), "CalculateRecruitableNumber")]
        public class CalculateRecruitableNumberPatch
        {
            private static bool Prefix(ref int __result, PartyBase party, CharacterObject character)
            {
                if (party.MobileParty.IsMainParty)
                {
                    if (character.IsHero || party.PrisonRoster.Count == 0 || party.PrisonRoster.TotalRegulars <= 0)
                    {
                        return true;
                    }
                    __result = party.PrisonRoster.GetElementNumber(character);
                    return false;
                }

                return true;
            }
        }
    }
}
