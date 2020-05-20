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

            private static bool Prefix(ref float __result, MobileParty mobileParty, float baseSpeed, StatExplainer explanation)
            {
                PartyBase party = mobileParty.Party;
                if (party == PartyBase.MainParty)
                {
                    ExplainedNumber explainedNumber = new ExplainedNumber(baseSpeed, explanation, null);
                    TerrainType faceTerrainType = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(mobileParty.CurrentNavigationFace);
                    if (faceTerrainType == TerrainType.Forest)
                    {
                        explainedNumber.AddFactor(-0.3f, _movingInForest);
                        PerkHelper.AddFeatBonusForPerson(DefaultFeats.Cultural.BattanianForestAgility, mobileParty.Leader, ref explainedNumber);
                    }
                    else if (faceTerrainType == TerrainType.Water || faceTerrainType == TerrainType.River || faceTerrainType == TerrainType.Bridge || faceTerrainType == TerrainType.ShallowRiver)
                    {
                        explainedNumber.AddFactor(-0.3f, _fordEffect);
                    }
                    if (Campaign.Current.IsNight)
                    {
                        explainedNumber.AddFactor(-0.25f, _night);
                    }
                    if (faceTerrainType == TerrainType.Snow)
                    {
                        explainedNumber.AddFactor(-0.1f, _snow);
                        if (party.Leader != null)
                        {
                            PerkHelper.AddFeatBonusForPerson(DefaultFeats.Cultural.SturgianSnowAgility, party.Leader, ref explainedNumber);
                        }
                    }
                    explainedNumber.AddFactor(0.5f, _divineBlessing);
                    explainedNumber.LimitMin(1f);
                    __result = explainedNumber.ResultNumber;
                    return false;
                }
                return true;
            }
        }
    }
}
