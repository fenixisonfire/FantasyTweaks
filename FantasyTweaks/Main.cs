using System;
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

            var harmony = new Harmony("fantasytweaks.harmony.patch");
            harmony.PatchAll();

            InformationManager.DisplayMessage(new InformationMessage("Fantasy tweaks loaded."));
        }
    }

}
