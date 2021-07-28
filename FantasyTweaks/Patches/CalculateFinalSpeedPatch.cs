using HarmonyLib;
using TaleWorlds.Localization;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

namespace FantasyTweaks.Patches
{
    // TODO: Fix crash on load
    // [HarmonyPatch(typeof(DefaultPartySpeedCalculatingModel), "CalculateFinalSpeed")]
    public class CalculateFinalSpeedPatch
    {
            
        public static void Postfix(MobileParty mobileParty, ref ExplainedNumber __result)
        {
            __result.Add(0.5f, new TextObject("{}Something", null));
        }
    }
}
