using BepInEx.Logging;

using HarmonyLib;

using System.Linq;

namespace TestPlugin.Patches;

[HarmonyPatch(typeof(InventoryScreenInput))]
[HarmonyPatch("OnItem2dClick")]
internal class CtrlClickItemTransferPatch
{
    private static void Postfix(ref tk2dUIItem tk2ditem)
    {
        var logger = (ManualLogSource)Logger.Sources.First(l => l.SourceName == PluginInfo.PLUGIN_NAME);

        if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftControl))
        {
            var item = tk2ditem as InventoryItem2dInput;
            if(item is null)
            {
                return;
            }

            if(item.InVicinity)
            {
                GameManager.PlayerInventory.AddItem(item.InventoryItem);
            }
            else
            {
                GameManager.PlayerInventory.Remove(item.InventoryItem);
            }
        }
    }
}
