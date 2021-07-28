using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace FantasyTweaks.Patches
{
    
    [HarmonyPatch(typeof(PartyScreenLogic), "ExecuteTroop")]
    public class ExecuteTroopPatch
    {
        public static void Postfix(PartyScreenLogic.PartyCommand command)
        {
            CharacterObject character = command.Character;

            Equipment battleEquipment = character.HeroObject.BattleEquipment;
                
            for (int i = 0; i < Equipment.EquipmentSlotLength - 1; i++)
            {
                ItemObject item = battleEquipment[i].Item;
                if (item != null)
                {
                    PartyBase.MainParty.ItemRoster.AddToCounts(item, 1);
                    InformationManager.DisplayMessage(
                        new InformationMessage($"Obtained {item.Name}")
                    );
                }
            }

        }

    }
}
