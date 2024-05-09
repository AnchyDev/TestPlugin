using BepInEx.Logging;

using GP.Utility;

using HarmonyLib;

using System.Linq;

using UnityEngine;

namespace TestPlugin.Patches;

[HarmonyPatch(typeof(InventoryScreenInput))]
[HarmonyPatch("OnItem2dClick")]
internal class CtrlClickItemTransferPatch
{
    private static void Postfix(ref tk2dUIItem tk2ditem)
    {
        var logger = (ManualLogSource)BepInEx.Logging.Logger.Sources.First(l => l.SourceName == PluginInfo.PLUGIN_NAME);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            var item = tk2ditem as InventoryItem2dInput;
            if(item is null)
            {
                return;
            }

            var pInventory = GameManager.PlayerInventory;

            // Item is in vicinity, move to character inventory.
            if(item.InVicinity)
            {
                pInventory.AddItem(item.InventoryItem);
                return;
            }

            var inventory = GameManager.InventoryScreenInput;
            var bag = inventory.ActiveBagItem;

            // No opened container found, moving to vicinity
            if (bag is null)
            {
                pInventory.Remove(item.InventoryItem);
                return;
            }

            for (int i = 0; i < bag.BagItems.Length; i++)
            {
                if(bag.AddItemToSlot(item.InventoryItem, i))
                {
                    item.RemoveFromParentSlot();
                    item.SetParentSlot(null);

                    // If it is a bag item, remove bag from expandable area.
                    if (item.ExpandableArea is not null)
                    {
                        RemoveBag(item, inventory);
                    }

                    item.gameObject.SafeDestroy();

                    pInventory.UpdateVisualModel(false);
                    inventory.ActiveBagItem.UpdateItems();

                    // Re-push the bag to update items.
                    GameManager.PopInGameScreen();
                    GameManager.PushInventoryScreen(bag);

                    break;
                }
            }
        }
    }

    private static void ExpandableAreaChanged(InventoryScreenInput inventory)
    {
        var area = inventory.BagItemExpandableAreas[0];

        float num = 0f;
        inventory.BagsScrollableArea.contentContainer.transform.localPosition = new Vector3(0.62f, -17.39f, -0.02f);
        if (inventory.BagItemExpandableAreas.Count > 0)
        {
            bool flag = false;
            inventory.BagItemExpandableAreas[0].transform.localPosition = new Vector3(0f, -2.5f, 0f);
            for (int i = 0; i < inventory.BagItemExpandableAreas.Count; i++)
            {
                if (flag)
                {
                    Vector3 localPosition = inventory.BagItemExpandableAreas[i].transform.localPosition;
                    localPosition.y = inventory.BagItemExpandableAreas[i - 1].transform.localPosition.y - inventory.BagItemExpandableAreas[i - 1].Length;
                    inventory.BagItemExpandableAreas[i].transform.localPosition = localPosition;
                }
                else if (inventory.BagItemExpandableAreas[i] == area)
                {
                    flag = true;
                }
                num += inventory.BagItemExpandableAreas[i].Length;
            }
        }
        inventory.BagsScrollableArea.ContentLength = num;
    }

    private static void RemoveBag(InventoryItem2dInput item, InventoryScreenInput inventory)
    {
        inventory.BagItemExpandableAreas.Remove(item.ExpandableArea);

        if (inventory.BagItemExpandableAreas.Count > 0)
        {
            // Notify expanded area changed
            ExpandableAreaChanged(inventory);
        }
        else
        {
            inventory.BagsScrollableArea.ContentLength -= item.ExpandableArea.Length;
        }

        item.ExpandableArea.gameObject.SafeDestroy();
        item.ExpandableArea = null;
    }
}
