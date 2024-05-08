namespace TestPlugin.Commands;

internal class HelpCommand : ICommand
{
    public string Command => "help";
    public string Description => "Prints out all the available commands.";

    public CommandResult Process(string command, string[] args)
    {
        var handler = CommandHandler.Instance;

        foreach(var cmd in handler.Commands)
        {
            GameManager.ChatScreenInput.AddText($"{cmd.Key}: {cmd.Value.Description}");
        }

        return new CommandResult(true);
    }
}
