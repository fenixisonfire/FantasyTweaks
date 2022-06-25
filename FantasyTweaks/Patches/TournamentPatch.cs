using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Library;

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

    [HarmonyPatch(typeof(TournamentManager))]
    public class TournamentBehaviorPatch
    {
        private static readonly int TOURNAMENT_WIN_AWARD = 1000;

        [HarmonyPrefix]
        [HarmonyPatch("OnPlayerWinTournament")]
        static bool OnPlayerWinTournamentPatchPrefix(TournamentManager __instance)
        {
            Hero.MainHero.ChangeHeroGold(TOURNAMENT_WIN_AWARD);
            InformationManager.DisplayMessage(new InformationMessage("Congratulations on your winnings!"));
            return true;
        }
    }
}
