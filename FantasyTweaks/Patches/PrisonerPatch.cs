using System;
using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;

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
            private static readonly float[] RECRUITMENT_PROBABILITY = new float[7] { 1f, 0.9f, 0.8f, 0.7f, 0.6f, 0.5f, 0.4f };

            public static bool Prefix(ref float[] __result, MobileParty mainParty)
            {
                if (mainParty.IsMainParty)
                {
                    __result = RECRUITMENT_PROBABILITY;
                    return false;
                }
                
                return true;
            }
        }

        [HarmonyPatch(typeof(RecruitPrisonersCampaignBehavior), "DailyTick")]
        public class DailyTickPatch
        {
            private static readonly float CONVERSION_RATE_MIN = 0.4f;

            private static bool Prefix(RecruitPrisonersCampaignBehavior __instance)
            {
                TroopRoster prisonRoster = MobileParty.MainParty.PrisonRoster;
                for (int i = 0; i < prisonRoster.Count; i++)
                {
                    CharacterObject character = prisonRoster.GetCharacterAtIndex(i);
                    int currentlyRecruitable = __instance.GetRecruitableNumber(character);
                    int totalTroopCount = prisonRoster.GetTroopCount(character);
                    int newRecruitable = currentlyRecruitable + (int) Math.Ceiling(MBRandom.RandomFloatRanged(CONVERSION_RATE_MIN, 1.0f) * (totalTroopCount - currentlyRecruitable));
                    __instance.SetRecruitableNumber(character, newRecruitable);
                }

                return false;
            }
        }
       
    }
}
