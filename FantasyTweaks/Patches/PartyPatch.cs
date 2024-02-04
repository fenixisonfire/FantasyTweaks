using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace FantasyTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "GetPartyMemberSizeLimit")]
    internal class PartySizePatch
    {
        private static readonly int _ultimateLeaderSkillCutOff = 250;
        private static readonly TextObject _textCustodianGuard = new TextObject("{=SBIh8N1p}Custodian Guard");
        
        public static void Postfix(PartyBase party, bool includeDescriptions, ref ExplainedNumber __result)
        {
            if (!party.MobileParty.IsMainParty)
            {
                return;
            }
            
            Hero hero = party.LeaderHero;
            bool hasUltimateLeaderPerk = hero.GetPerkValue(DefaultPerks.Leadership.UltimateLeader);
            int leadershipSkill = hero.GetSkillValue(DefaultSkills.Leadership);
            if (hasUltimateLeaderPerk)
            {
                int bonus = Math.Max(leadershipSkill - _ultimateLeaderSkillCutOff, 0);
                __result.Add(bonus, _textCustodianGuard);
            }
        }
    }
}
