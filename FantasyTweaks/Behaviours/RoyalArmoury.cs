using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;

namespace FantasyTweaks.Behaviours
{
    class RoyalArmoury : CampaignBehaviorBase
    {
        private static readonly string TOWN_KEEP_MENU_ID = "town_keep";
        private static readonly int INDEX = 99;

        private static Random RANDOM = new Random();
        private static readonly int MIN_ITEM_COUNT = 0;
        private static readonly int MAX_ITEM_COUNT = 5;

        private static readonly List<string> CULTURES = new List<string>(new string[] { CampaignData.CultureBattania, CampaignData.CultureAserai, CampaignData.CultureSturgia, CampaignData.CultureVlandia, CampaignData.CultureEmpire, CampaignData.CultureKhuzait });

        private static readonly List<string> ASERAI_ITEMS = new List<string>() {"southern_noble_helmet",
                                                                                "southern_lord_helmet",
                                                                                "aserai_lord_helmet_a",
                                                                                "desert_scale_shoulders",
                                                                                "leopard_pelt",
                                                                                "desert_scale_armor",
                                                                                "sturgia_cavalry_armor",
                                                                                "desert_lamellar",
                                                                                "stitched_leather_over_mail",
                                                                                "mail_and_plate_barding",
                                                                                "half_mail_and_plate_barding",
                                                                                "eastern_javelin_3_t4",
                                                                                "steel_round_shield",
                                                                                "southern_broad_2hsword_t4",
                                                                                "aserai_noble_sword_2_t5",
                                                                                "longbow_recurve_desert_bow",
                                                                                "desert_war_horse" };

        private static readonly List<string> BATTANIA_ITEMS = new List<string>() {"battanian_crowned_helmet",
                                                                                  "battanian_plated_noble_helmet",
                                                                                  "battanian_noble_helmet_with_feather",
                                                                                  "battania_warlord_pauldrons",
                                                                                  "armored_bearskin",
                                                                                  "bearskin",
                                                                                  "rough_bearskin",
                                                                                  "battania_warlord_armor",
                                                                                  "battania_mercenary_armor",
                                                                                  "kilt_over_plated_leather",
                                                                                  "battania_warlord_bracers",
                                                                                  "battania_noble_bracers",
                                                                                  "battania_warlord_boots",
                                                                                  "battania_horse_harness_scaled",
                                                                                  "battania_horse_harness_halfscaled",
                                                                                  "bodkin_arrows_a",
                                                                                  "battania_polearm_1_t5",
                                                                                  "early_retirement_2hsword_t3",
                                                                                  "noble_long_bow",
                                                                                  "noble_horse_battania" };

        private static readonly List<string> EMPIRE_ITEMS = new List<string>() {"empire_guarded_lord_helmet",
                                                                                "empire_jewelled_helmet",
                                                                                "empire_lord_helmet",
                                                                                "empire_helmet_with_metal_strips",
                                                                                "imperial_goggled_helmet",
                                                                                "imperial_lamellar_shoulders",
                                                                                "pauldron_cape_a",
                                                                                "studded_imperial_neckguard",
                                                                                "imperial_scale_armor",
                                                                                "lamellar_with_scale_skirt",
                                                                                "imperial_lamellar",
                                                                                "legionary_mail",
                                                                                "lamellar_plate_gauntlets",
                                                                                "decorated_imperial_gauntlets",
                                                                                "lordly_padded_mitten",
                                                                                "lamellar_plate_boots",
                                                                                "decorated_imperial_boots",
                                                                                "half_scale_barding",
                                                                                "imperial_scale_barding",
                                                                                "arrow_emp_1_a",
                                                                                "tall_heater_shield" };

        private static readonly List<string> KHUZAIT_ITEMS = new List<string>() {"khuzait_noble_helmet_with_neckguard",
                                                                                 "spiked_helmet_with_facemask",
                                                                                 "khuzait_noble_helmet_with_fur",
                                                                                 "eastern_vendel_helmet",
                                                                                 "khuzait_noble_helmet_with_feathers",
                                                                                 "brass_lamellar_shoulder",
                                                                                 "lamellar_shoulders",
                                                                                 "brass_lamellar_over_mail",
                                                                                 "eastern_plated_leather",
                                                                                 "eastern_plated_leather_vambraces",
                                                                                 "studded_steppe_barding",
                                                                                 "steppe_half_barding",
                                                                                 "heavy_steppe_arrows",
                                                                                 "steel_round_shield",
                                                                                 "khuzait_noble_sword_2_t5",
                                                                                 "noble_bow",
                                                                                 "noble_horse" };

        private static readonly List<string> STURGIA_ITEMS = new List<string>() {"sturgian_lord_helmet_c",
                                                                                 "lendman_helmet_over_full_mail",
                                                                                 "lendman_helmet_over_mail",
                                                                                 "northern_warlord_helmet",
                                                                                 "sturgian_lord_helmet_b",
                                                                                 "sturgian_helmet_b_close",
                                                                                 "sturgian_lord_helmet_a",
                                                                                 "imperial_goggled_helmet",
                                                                                 "decorated_goggled_helmet",
                                                                                 "brass_lamellar_shoulder_white",
                                                                                 "brass_scale_shoulders",
                                                                                 "armored_bearskin",
                                                                                 "bearskin",
                                                                                 "rough_bearskin",
                                                                                 "sturgian_fortified_armor",
                                                                                 "sturgian_lamellar_fortified",
                                                                                 "sturgian_lamellar_base",
                                                                                 "northern_coat_of_plates",
                                                                                 "northern_brass_lamellar_over_mail",
                                                                                 "northern_plated_gloves",
                                                                                 "northern_plated_boots",
                                                                                 "northern_ring_barding",
                                                                                 "sturgia_polearm_1_t5",
                                                                                 "storm_charger"};

        private static readonly List<string> VLANDIA_ITEMS = new List<string>() {"western_crowned_plated_helmet",
                                                                                 "western_crowned_helmet",
                                                                                 "full_helm_over_mail_coif",
                                                                                 "western_plated_helmet",
                                                                                 "mail_shoulders",
                                                                                 "coat_of_plates_over_mail",
                                                                                 "plated_leather_coat",
                                                                                 "lordly_mail_mitten",
                                                                                 "reinforced_mail_mitten",
                                                                                 "lordly_padded_mitten",
                                                                                 "strapped_mail_chausses",
                                                                                 "mail_chausses",
                                                                                 "chain_barding",
                                                                                 "halfchain_barding",
                                                                                 "vlandia_2hsword_1_t5",
                                                                                 "crossbow_f",
                                                                                 "noble_horse" };

        private static readonly Dictionary<string, List<string>> CULTURE_TO_ITEMS_DICT = new Dictionary<string, List<string>>()
        {
            { CampaignData.CultureAserai, ASERAI_ITEMS },
            { CampaignData.CultureBattania, BATTANIA_ITEMS },
            { CampaignData.CultureEmpire, EMPIRE_ITEMS },
            { CampaignData.CultureKhuzait, KHUZAIT_ITEMS },
            { CampaignData.CultureSturgia, STURGIA_ITEMS },
            { CampaignData.CultureVlandia, VLANDIA_ITEMS }
        };

        private void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
        {
            campaignGameStarter.AddGameMenuOption(TOWN_KEEP_MENU_ID, "armoury", "Access the Armoury", new GameMenuOption.OnConditionDelegate(this.AccessOnCondition), new GameMenuOption.OnConsequenceDelegate(this.ArmouryOnConsequence), false, INDEX, false);
        }

        private bool AccessOnCondition(MenuCallbackArgs args)
        {
            args.optionLeaveType = GameMenuOption.LeaveType.Trade;
            return true;
        }

        private void ArmouryOnConsequence(MenuCallbackArgs args)
        {
            ItemRoster itemRoster = new ItemRoster();
            String culture = Settlement.CurrentSettlement.Culture.StringId;

            List<string> items = new List<string>();
            if (CULTURE_TO_ITEMS_DICT.TryGetValue(culture, out items))
            {
                items.ForEach(item =>
                {
                    itemRoster.AddToCounts(MBObjectManager.Instance.GetObject<ItemObject>(item), RANDOM.Next(MIN_ITEM_COUNT, MAX_ITEM_COUNT));
                });
            }
            InventoryManager.OpenScreenAsTrade(itemRoster, Settlement.CurrentSettlement.Town, InventoryManager.InventoryCategoryType.None, null);
        }

        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
        }

        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}
