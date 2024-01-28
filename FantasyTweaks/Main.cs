using System;
using HarmonyLib;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using FantasyTweaks.Behaviours;
using FantasyTweaks.Models;
using TaleWorlds.Library;

namespace FantasyTweaks
{
    internal class Main : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            try
            {
                var harmony = new Harmony("fantasytweaks.harmony.patch");

                // Harmony.DEBUG = true;
                harmony.PatchAll();
            }
            catch (Exception ex)
            {
                InformationManager.DisplayMessage(
                    new InformationMessage($"Error occured while loading Fantasy Tweaks:\n {ex}")
                );
            }
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            InformationManager.DisplayMessage(new InformationMessage("Fantasy Tweaks 1.0.0"));
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);
            if (game.GameType is Campaign)
            {
                CampaignGameStarter campaignGameStarter = (CampaignGameStarter)gameStarterObject;
                campaignGameStarter.AddBehavior(new RoyalArmoury());
                campaignGameStarter.AddBehavior(new StoneOfRenewal());

                gameStarterObject.AddModel(new Shadowfax());
            }
        }
    }

}
