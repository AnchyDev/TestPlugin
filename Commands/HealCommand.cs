using UnityEngine;

namespace TestPlugin.Commands;

internal class HealCommand : ICommand
{
    public string Command => "heal";
    public string Description => "Restores the players vitals to healthy.";

    public CommandResult Process(string command, string[] args)
    {
        var playerComponent = GameObject.FindObjectOfType<LocalPlayerController>();
        if (!playerComponent)
        {
            return new CommandResult(false);
        }

        playerComponent.PlayerVitals.Reset();

        return new CommandResult(true, "Restored player vitals to healthy.");
    }
}
