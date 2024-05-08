using UnityEngine;

namespace TestPlugin.Commands;

internal class FogCommand : ICommand
{
    public string Command => "fog";
    public string Description => "Toggled fog on/off";

    public CommandResult Process(string command, string[] args)
    {
        var fogComponent = GameObject.FindObjectOfType<FogManager>();
        if (!fogComponent)
        {
            return new CommandResult(false);
        }

        RenderSettings.fog = !RenderSettings.fog;
        GameManager.LocalPlayerController.CameraSettings.DrawDistance = 2000;

        return new CommandResult(true);
    }
}
