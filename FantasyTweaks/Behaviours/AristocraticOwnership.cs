using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Election;
using TaleWorlds.Library;

namespace FantasyTweaks.Behaviours
{
    internal class AristocraticOwnership : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.HourlyTickEvent.AddNonSerializedListener(this, new Action(this.TerminateFiefVotes));
        }

        private void TerminateFiefVotes()
        {
            if (!Hero.MainHero.IsFactionLeader)
            {
                return;
            }

            Kingdom kingdom = Hero.MainHero.Clan.Kingdom;
            if (kingdom == null)
            {
                return;
            }

            if (!Hero.MainHero.GetPerkValue(DefaultPerks.Steward.PriceOfLoyalty))
            {
                return;
            }
            
            List<KingdomDecision> cancellableDecisions = new List<KingdomDecision>();
            foreach (KingdomDecision decision in kingdom.UnresolvedDecisions)
            {
                if (decision is SettlementClaimantDecision)
                {
                    cancellableDecisions.Add(decision);
                }
            }
            
            foreach (KingdomDecision decision in cancellableDecisions)
            {
                SettlementClaimantDecision settlementClaimantDecision = (SettlementClaimantDecision)decision;
                string settlementname = settlementClaimantDecision.Settlement.GetName().ToString();
                settlementClaimantDecision.Settlement.Town.IsOwnerUnassigned = false;
                kingdom.RemoveDecision(decision);
                InformationManager.DisplayMessage(
                    new InformationMessage($"{kingdom.GetName()} has claimed {settlementname} through Aristocratic Ownership.")
                );
            }
        }

        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}
