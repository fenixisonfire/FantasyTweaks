using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using SandBox.TournamentMissions.Missions;

namespace FantasyTweaks.Patches
{
    public class TournamentPatch
    {
        private static readonly int TOURNAMENT_RENOWN_MULTIPLIER = 2;
        private static readonly int TOURNAMENT_WIN_REWARD = 1000;

        [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
        public class GetRenownRewardPatch
        {
            private static void Postfix(ref int __result)
            {
                __result *= TOURNAMENT_RENOWN_MULTIPLIER;
            }
        }

        [HarmonyPatch(typeof(TournamentBehavior), "OnPlayerWinTournament")]
        public class OnPlayWinTournamentPatch
        {
            private static bool Prefix(TournamentBehavior __instance)
            {
                typeof(TournamentBehavior).GetProperty("OverallExpectedDenars").SetValue(__instance, __instance.OverallExpectedDenars + TOURNAMENT_WIN_REWARD);
                return true;
            }
        }
    }
}
