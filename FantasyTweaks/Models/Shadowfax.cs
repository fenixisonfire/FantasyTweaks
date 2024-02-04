using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;

namespace FantasyTweaks.Models
{
    public class Shadowfax : DefaultPartySpeedCalculatingModel
    {
        private static readonly float SHADOWFAX_MOVEMENT_BONUS = 1.0f;

        public override ExplainedNumber CalculateFinalSpeed(MobileParty mobileParty, ExplainedNumber finalSpeed)
        {
            ExplainedNumber result = base.CalculateFinalSpeed(mobileParty, finalSpeed);

            if (mobileParty.Party == PartyBase.MainParty)
            {
                result.AddFactor(SHADOWFAX_MOVEMENT_BONUS);
            }
            
            result.LimitMin(1f);
            return result;
        }
    }
}
