using HarmonyLib;

namespace TestPlugin.Patches;

[HarmonyPatch(typeof(PlayerInventory))]
[HarmonyPatch("AddItem")]
internal class ChatNotifyItemPickedUp
{
    private static void Postfix(ref InventoryItem item)
    {
        if(item is null)
        {
            return;
        }

        GameManager.ChatScreenInput.AddText($"Picked up item", UnityEngine.Color.white, false);
        GameManager.ChatScreenInput.AddText(item.ItemDisplayName, GetColorFromItemRarity(item.Rarity));
    }

    private static UnityEngine.Color GetColorFromItemRarity(InventoryItem.RarityType rarity)
    {
        switch(rarity)
        {
            case InventoryItem.RarityType.Common:
                return UnityEngine.Color.white;

            case InventoryItem.RarityType.Uncommon:
                return UnityEngine.Color.cyan;

            case InventoryItem.RarityType.Rare:
                return UnityEngine.Color.magenta;
        }

        return UnityEngine.Color.white;
    }
}
