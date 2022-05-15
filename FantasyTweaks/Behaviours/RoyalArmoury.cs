using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;

namespace FantasyTweaks.Behaviours
{

    /**
     * New shop added to keeps that stocks high end items.
     */
    class RoyalArmoury : CampaignBehaviorBase
    {
        private static readonly string TOWN_KEEP_MENU_ID = "town_keep";

        private static readonly Random RANDOM = new Random();
        private static readonly int MIN_ITEM_SELECTION_COUNT = 10;
        private static readonly int MAX_ITEM_SELECTION_COUNT = 31;
        private static readonly int MIN_ITEM_COUNT = 1;
        private static readonly int MAX_ITEM_COUNT = 6;

        private static readonly List<string> CULTURES = new List<string>(new string[] { CampaignData.CultureBattania, CampaignData.CultureAserai, CampaignData.CultureSturgia, CampaignData.CultureVlandia, CampaignData.CultureEmpire, CampaignData.CultureKhuzait });

        private static readonly List<string> ASERAI_ITEMS = new List<string>() 
        {
            "southern_noble_helmet",
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
            "desert_war_horse" 
        };

        private static readonly List<string> BATTANIA_ITEMS = new List<string>() 
        {
            "battanian_crowned_helmet",
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
            "noble_horse_battania" 
        };

        private static readonly List<string> EMPIRE_ITEMS = new List<string>() 
        {
            "empire_guarded_lord_helmet",
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
            "arrow_emp_1_a"
        };

        private static readonly List<string> KHUZAIT_ITEMS = new List<string>() 
        {
            "khuzait_noble_helmet_with_neckguard",
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
            "noble_horse" 
        };

        private static readonly List<string> STURGIA_ITEMS = new List<string>() 
        {
            "sturgian_lord_helmet_c",
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
            "storm_charger"
        };

        private static readonly List<string> VLANDIA_ITEMS = new List<string>() 
        {
            "western_crowned_plated_helmet",
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
            "noble_horse" 
        };

        private static readonly List<string> SWADIAN_ITEMS = new List<string>()
        {
            "coat_of_plates1_c",
            "coat_of_plates2_c",
            "coat_of_plates3_c",
            "coat_of_plates4_c",
            "coat_of_plates5_c",
            "swadian_brass_plate_armor",
            "swadian_leather_plate_armor",
            "swadian_plate_armor",
            "swadian_striped_plate_armor",
            "swadian_plate_with_armguards",
            "swadian_leather_plate_with_armguards",
            "swadian_striped_cuirass",
            "swadian_cuirass",
            "engraved_swadian_shoulders",
            "swadian_shoulders",
            "great_helm_base",
            "winged_great_helm_base",
            "great_helm_brass",
            "winged_great_helm_brass",
            "great_helm_dark",
            "winged_great_helm_dark",
            "royal_great_helm",
            "royal_great_helm_brass",
            "sallet_base",
            "sallet_crowned",
            "sallet_wings",
            "pigface_bascinet",
            "pigface_bascinet_feather",
            "pigface_bascinet_tail",
            "vlandian_helm_with_faceplate",
            "vlandian_helm_with_faceplate_blackend_brass",
            "vlandian_helm_with_faceplate_brass",
            "vlandian_helm_with_faceplate_painted",
            "italio_norman_helm_crown",
            "italio_norman_helm_bronze",
            "italio_norman_helm",
            "crowned_helm_with_brass_faceplate",
            "crowned_helm_with_faceplate",
            "fluted_helm_with_brass_faceplate",
            "fluted_helm_with_faceplate",
            "vlandian_helm_with_visor",
            "engraved_vlandian_helm_with_visor",
            "crowned_royal_vlandian_helm_with_visor",
            "crowned_vlandian_helm_with_visor",
            "great_prankh_helm",
            "great_prankh_helm_crest",
            "great_prankh_helm_winged",
            "great_prankh_helm_horns",
            "great_prankh_helm_royal",
            "Western_chain_shoulders",
            "mail_with_tabbard1",
            "mail_with_tabbard2",
            "mail_with_tabbard3",
            "mail_with_tabbard4",
            "mail_with_tabbard5",
            "Pothelm",
            "Pothelm2",
            "Sugarloaf_helm",
            "Sugarloaf_helm2",
            "Sugarloaf_helm3",
            "Sugarloaf_helm4",
            "plate_boots",
            "plate_boots2",
            "sa_bacinet_with_facemask"
        };

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
            campaignGameStarter.AddGameMenuOption(
                TOWN_KEEP_MENU_ID, 
                "armoury", 
                "Access the Armoury", 
                new GameMenuOption.OnConditionDelegate(this.AccessOnCondition), 
                new GameMenuOption.OnConsequenceDelegate(this.ArmouryOnConsequence), 
                false, 
                -1, 
                false
            );
        }

        private bool AccessOnCondition(MenuCallbackArgs args)
        {
            args.optionLeaveType = GameMenuOption.LeaveType.Trade;
            return true;
        }

        private void ArmouryOnConsequence(MenuCallbackArgs args)
        {
            ItemRoster itemRoster = new ItemRoster();
            string culture = Settlement.CurrentSettlement.Culture.StringId;

            List<string> cultureItems = new List<string>();
            CULTURE_TO_ITEMS_DICT.TryGetValue(culture, out cultureItems);

            int itemSelectionCount = RANDOM.Next(MIN_ITEM_SELECTION_COUNT, MAX_ITEM_SELECTION_COUNT);
            for (int i = 0; i < itemSelectionCount; i++)
            {
                ItemObject cultureItem = MBObjectManager.Instance.GetObject<ItemObject>(cultureItems.GetRandomElement());
                if (cultureItem != null)
                {
                    itemRoster.AddToCounts(cultureItem, RANDOM.Next(MIN_ITEM_COUNT, MAX_ITEM_COUNT));
                }
                ItemObject swadianItem = MBObjectManager.Instance.GetObject<ItemObject>(SWADIAN_ITEMS.GetRandomElement());
                if (swadianItem != null)
                {
                    itemRoster.AddToCounts(swadianItem, RANDOM.Next(MIN_ITEM_COUNT, MAX_ITEM_COUNT));
                }
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
