using System;
using System.Reflection;
using HarmonyLib;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Core;

namespace FantasyTweaks
{
    internal class Main : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            try
            {
                var harmony = new Harmony("fantasytweaks.harmony.patch");
                harmony.PatchAll();
            }
            catch (Exception ex)
            {
                InformationManager.DisplayMessage(
                    new InformationMessage($"Error occured while loading Fantasy Tweaks:\n {ex.ToString()}")
                );
            }

        }
    }

}
