using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using SandBox.TournamentMissions.Missions;

namespace FantasyTweaks.Patches
{
    internal class TournamentPatch
    {
        [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
        public class GetRenownRewardPatch
        {
            private static void Postfix(ref int __result)
            {
                __result = __result * 2;
            }
        }

        [HarmonyPatch(typeof(TournamentBehavior), "get_OverallExpectedDenars")]
        public class GetOverallExpectedDenarsPatch
        {
            private static void Postfix(ref int __result)
            {
                __result = __result + 1000;
            }
        }
    }
}
