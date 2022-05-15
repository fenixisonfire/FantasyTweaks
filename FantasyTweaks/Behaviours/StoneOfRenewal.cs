using System;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace FantasyTweaks.Behaviours
{

    /**
     * New option added to keeps to allow character respec
     */
    class StoneOfRenewal : CampaignBehaviorBase
    {
        private static readonly string TOWN_KEEP_MENU_ID = "town_keep";

        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.AddStoneOfRenewalMenuItems));
        }

        private void AddStoneOfRenewalMenuItems(CampaignGameStarter campaignGameStarter)
        {
            campaignGameStarter.AddGameMenuOption(
                TOWN_KEEP_MENU_ID, 
                "stone_of_renewal", 
                "Touch the Stone of Renewal", 
                new GameMenuOption.OnConditionDelegate(this.AccessOnCondition),
                new GameMenuOption.OnConsequenceDelegate(this.RespecOnConsequence),
                false, 
                -1, 
                false
            );
        }

        private bool AccessOnCondition(MenuCallbackArgs args)
        {
            args.optionLeaveType = GameMenuOption.LeaveType.Submenu;
            return true;
        }

        private void RespecOnConsequence(MenuCallbackArgs args)
        {
            InformationManager.ShowInquiry(
                new InquiryData(
                    "Stone of Renewal",
                    "Touching the stone of renewal clears one's mind of previously learnt perks. Are you sure you wish to continue?",
                    true,
                    true,
                    "Yes",
                    "No",
                    new Action(RespecMainHero),
                    null
                )
            );
        }

        private void RespecMainHero()
        {
            RespecHero(Hero.MainHero);
        }

        private void RespecHero(Hero hero)
        {
            if (hero.GetPerkValue(DefaultPerks.Crafting.VigorousSmith))
            {
                SetAttributeValue(hero, DefaultCharacterAttributes.Vigor, hero.GetAttributeValue(DefaultCharacterAttributes.Vigor) - 1);
            }

            if (hero.GetPerkValue(DefaultPerks.Crafting.StrongSmith))
            {
                SetAttributeValue(hero, DefaultCharacterAttributes.Control, hero.GetAttributeValue(DefaultCharacterAttributes.Control) - 1);
            }

            if (hero.GetPerkValue(DefaultPerks.Crafting.EnduringSmith))
            {
                SetAttributeValue(hero, DefaultCharacterAttributes.Endurance, hero.GetAttributeValue(DefaultCharacterAttributes.Endurance) - 1);
            }

            if (hero.GetPerkValue(DefaultPerks.Athletics.HealthyCitizens))
            {
                SetAttributeValue(hero, DefaultCharacterAttributes.Endurance, hero.GetAttributeValue(DefaultCharacterAttributes.Endurance) - 1);
            }

            if (hero.GetPerkValue(DefaultPerks.Athletics.Steady))
            {
                SetAttributeValue(hero, DefaultCharacterAttributes.Control, hero.GetAttributeValue(DefaultCharacterAttributes.Control) - 1);
            }

            if (hero.GetPerkValue(DefaultPerks.Athletics.Strong))
            {
                SetAttributeValue(hero, DefaultCharacterAttributes.Vigor, hero.GetAttributeValue(DefaultCharacterAttributes.Vigor) - 1);
            }

            if (hero.GetPerkValue(DefaultPerks.Crafting.WeaponMasterSmith))
            {
                hero.HeroDeveloper.AddFocus(DefaultSkills.OneHanded, -1, false);
                hero.HeroDeveloper.AddFocus(DefaultSkills.TwoHanded, -1, false);
            }

            hero.ClearPerks();
            InformationManager.DisplayMessage(new InformationMessage(hero.Name + " has touched the Stone of Renewal. They shall begin anew."));
        }

        public override void SyncData(IDataStore dataStore)
        {
        }

        private static void SetAttributeValue(Hero hero, CharacterAttribute characterAttribute, int value)
        {
            object[] parameters = new object[]
            {
                characterAttribute,
                value
            };

            MethodInfo[] methods = typeof(Hero).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach(MethodInfo mi in methods)
            {
                if (mi.Name == "SetAttributeValueInternal")
                {
                    mi.Invoke(hero, parameters);
                    break;
                }
            }
        }
    }
}
