using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using SandBox.TournamentMissions.Missions;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace FantasyTweaks.Patches
{

    [HarmonyPatch(typeof(DefaultTournamentModel))]
    public class DefaultTournamentModelPatch
    {
        private static readonly int TOURNAMENT_RENOWN_MULTIPLIER = 2;

        [HarmonyPostfix]
        [HarmonyPatch("GetRenownReward")]
        static void GetRenownRewardPostfix(Hero winner, Town town, ref int __result)
        {
            __result *= TOURNAMENT_RENOWN_MULTIPLIER;
        }
    }

    [HarmonyPatch(typeof(TournamentBehavior))]
    public class TournamentBehaviorPatch
    {
        private static readonly int TOURNAMENT_WIN_AWARD = 1000;

        [HarmonyPrefix]
        [HarmonyPatch("OnPlayerWinTournament")]
        static bool OnPlayerWinTournamentPatchPrefix(TournamentBehavior __instance)
        {
            typeof(TournamentBehavior).GetProperty("OverallExpectedDenars").SetValue(__instance, __instance.OverallExpectedDenars + TOURNAMENT_WIN_AWARD);
            InformationManager.DisplayMessage(new InformationMessage("Congratulations on your winnings!"));
            return true;
        }
    }
}
