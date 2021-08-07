using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;

namespace FantasyTweaks.Patches
{

    [HarmonyPatch(typeof(DefaultItemDiscardModel))]
    public class DefaultItemDiscardModelPatch
    {

        [HarmonyPostfix]
        [HarmonyPatch("GetXpBonusForDiscardingItem")]
        static void GetXpBonusForDiscardingItemPostfix(ItemObject item, ref int __result, int amount = 1)
        {
            __result = __result + item.Value * amount;
        }
    }
}
