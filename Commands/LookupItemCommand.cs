using System.Collections.Generic;

using UnityEngine;

namespace TestPlugin.Commands;

internal class LookupItemCommand : ICommand
{
    public string Command => "lookupitem";
    public string Description => "Allows you to lookup item ids by name.";

    public CommandResult Process(string command, string[] args)
    {
        if (args.Length < 2)
        {
            return new CommandResult(false, "Missing name argument. Example usage: .lookupitem sword");
        }

        var name = args[1];

        var items = Resources.LoadAll<InventoryItem>("Inventory");

        var foundItems = new List<InventoryItem>();

        foreach (var item in items)
        {
            if (item.ItemDisplayName.ToLower().Contains(name))
            {
                foundItems.Add(item);
            }
        }

        var chat = GameManager.ChatScreenInput;

        foreach (var item in foundItems)
        {
            
            chat.AddText($"{item.ItemTypeID} - {item.ItemDisplayName}");
        }

        chat.AddText($"Found '{foundItems.Count}' items with the name '{name}'.");

        return new CommandResult(true);
    }
}
