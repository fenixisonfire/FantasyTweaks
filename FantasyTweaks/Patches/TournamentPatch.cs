using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using SandBox.TournamentMissions.Missions;

namespace FantasyTweaks.Patches
{

    [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
    public class DefaultTournamentModelPatch
    {
        private static readonly int TOURNAMENT_RENOWN_MULTIPLIER = 2;

        static bool Prefix(ref int __result)
        {
            __result *= TOURNAMENT_RENOWN_MULTIPLIER;
            return false;
        }
    }

    [HarmonyPatch(typeof(TournamentBehavior), "OnPlayerWinTournament")]
    public class OnPlayerWinTournamentPatch
    {
        private static readonly int TOURNAMENT_WIN_AWARD = 1000;

        static bool Prefix(TournamentBehavior __instance)
        {
            typeof(TournamentBehavior).GetProperty("OverallExpectedDenars").SetValue(__instance, __instance.OverallExpectedDenars + TOURNAMENT_WIN_AWARD);
            return true;
        }
    }
}
