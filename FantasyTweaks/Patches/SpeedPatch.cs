using System.Collections.Generic;
using HarmonyLib;
using Helpers;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;


namespace FantasyTweaks.Patches
{
    public class SpeedPatch
    {
        [HarmonyPatch(typeof(DefaultPartySpeedCalculatingModel), "CalculateFinalSpeed")]
        public class CalculateFinalSpeedPatch
        {
            private static readonly TextObject _movingInForest = new TextObject("{=rTFaZCdY}Forest", null);
            private static readonly TextObject _fordEffect = new TextObject("{=NT5fwUuJ}Fording", null);
            private static readonly TextObject _night = new TextObject("{=fAxjyMt5}Night", null);
            private static readonly TextObject _snow = new TextObject("{=vLjgcdgB}Snow", null);
            private static readonly TextObject _divineBlessing = new TextObject("{}Divine Blessing", null);

            private static bool Prefix(ref float __result, MobileParty mobileParty, ExplainedNumber finalSpeed)
            {
                return true;
            }
        }
    }
}
