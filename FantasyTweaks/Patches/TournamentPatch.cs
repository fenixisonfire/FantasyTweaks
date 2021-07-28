using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.CampaignSystem.SandBox.Source.TournamentGames;
using SandBox.TournamentMissions.Missions;

namespace FantasyTweaks.Patches
{
    public class TournamentPatch
    {
        private static readonly int _tournamentRenownMultiplier = 2;
        private static readonly int _tournamentWinReward = 1000;

        [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
        public class GetRenownRewardPatch
        {
            private static void Postfix(ref int __result)
            {
                __result *= _tournamentRenownMultiplier;
            }
        }

        [HarmonyPatch(typeof(TournamentManager), "OnPlayerWinTournament")]
        public class OnPlayWinTournamentPatch
        {
            private static bool Prefix(TournamentBehavior __instance)
            {
                typeof(TournamentBehavior).GetProperty("OverallExpectedDenars").SetValue(__instance, __instance.OverallExpectedDenars + _tournamentWinReward);
                return true;
            }
        }
    }
}
