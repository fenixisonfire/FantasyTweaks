using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;

namespace FantasyTweaks.Patches
{
    public class PrisonerPatch
    {
        [HarmonyPatch(typeof(DefaultPrisonerRecruitmentCalculationModel), "GetDailyRecruitedPrisoners")]
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
            private static readonly float[] RECRUITMENT_PROBABILITY = new float[7] { 1F, 5F, 4F, 3F, 2F, 1F, 1F };

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
    }
}
