using BepInEx.Logging;
using GP.Utility;
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

            // Item is in vicinity, move to character inventory.
            if(item.InVicinity)
            {
                GameManager.PlayerInventory.AddItem(item.InventoryItem);
                return;
            }

            var bag = GameManager.InventoryScreenInput.ActiveBagItem;

            // No bag found, moving to vicinity
            if (bag is null)
            {
                GameManager.PlayerInventory.Remove(item.InventoryItem);
                return;
            }

            logger.LogInfo($"Found bag: {bag.BagType}, {bag.ItemDisplayName}, Size: {bag.BagItems.Length}");


            // TODO: Fix this, when ctrl clicking items they are not moved to opened containers
            for (int i = 0; i < bag.BagItems.Length; i++)
            {
                if (bag.BagItems[i] is not null &&
                    !bag.BagItems[i].AddItem(item.InventoryItem))
                {
                    continue;
                }

                bag.AddItemToSlot(item.InventoryItem, i);
                item.SafeDestroy();

                break;
            }
        }
    }
}
