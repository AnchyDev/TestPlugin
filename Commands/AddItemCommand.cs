namespace TestPlugin.Commands;

public class AddItemCommand : ICommand
{
    public string Command => "additem";
    public string Description => "Adds items to the players inventory.";

    public CommandResult Process(string command, string[] args)
    {
        if (args.Length < 2)
        {
            return new CommandResult(false, "Missing item id argument. Example usage: .additem 123");
        }

        if (!int.TryParse(args[1], out int itemId))
        {
            return new CommandResult(false, "Invalid number format for item id.");
        }

        if (itemId > InventoryItem.ItemTypeIDToClassID.Length)
        {
            return new CommandResult(false, "Item not found.");
        }

        var item = InventoryItem.CreateLocalInstance(itemId);
        if (item is null)
        {
            return new CommandResult(false, "Failed to spawn item.");
        }

        GameManager.LocalPlayerController.PlayerInventory.AddItem(item);

        return new CommandResult(true, "Added item to inventory.");
    }
}
