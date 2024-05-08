namespace TestPlugin.Commands;

internal class GodCommand : ICommand
{
    public string Command => "god";
    public string Description => "Toggled god mode.";

    public CommandResult Process(string command, string[] args)
    {
        GameManager.LocalPlayerController.IsInvincible = !GameManager.LocalPlayerController.IsInvincible;

        return new CommandResult(true, $"God mode { (GameManager.LocalPlayerController.IsInvincible ? "enabled" : "disabled") }.");
    }
}
