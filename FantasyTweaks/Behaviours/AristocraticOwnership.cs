using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
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
                string title = "Settlement Election Cancelled";
                string message = "The election to determine the owner of " + settlementname + " has been cancelled. You may keep the settlement yourself or give it to a deserving vassal.";
                InformationManager.ShowInquiry(
                    new InquiryData(
                        title, 
                        message, 
                        true, // isAffirmativeOptionShown
                        false, // isNegativeOptionShown
                        "OK", // affirmativeText
                        "", // negativeText
                        null, // affirmativeAction
                        null // negativeAction
                    ), 
                    true // pauseGameActiveState
                );
            }
        }

        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}
