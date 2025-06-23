using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Localization;

namespace FantasyTweaks.Models
{
    public class Fleetfooted : DefaultPartySpeedCalculatingModel
    {
        private static readonly float FLEETFOOTED_MOVEMENT_BONUS = 2.0f;
        private static readonly TextObject _textFleetfooted = new TextObject("{=1rOKUMTM}Fleetfooted");

        public override ExplainedNumber CalculateFinalSpeed(MobileParty mobileParty, ExplainedNumber finalSpeed)
        {
            ExplainedNumber result = base.CalculateFinalSpeed(mobileParty, finalSpeed);

            if (mobileParty.Party == PartyBase.MainParty)
            {
                result.AddFactor(FLEETFOOTED_MOVEMENT_BONUS, _textFleetfooted);
            }

            result.LimitMin(1f);
            return result;
        }
    }
}