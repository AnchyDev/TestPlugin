using UnityEngine;

namespace TestPlugin.Commands;

internal class TimeCommand : ICommand
{
    public string Command => "time";
    public string Description => "Sets the time of day.";

    public CommandResult Process(string command, string[] args)
    {
        var natureComponent = GameObject.FindObjectOfType<Nature>();
        if (!natureComponent)
        {
            return new CommandResult(false);
        }

        natureComponent.SetWeatherPattern(-1);
        natureComponent.DayNight.TimeOfDay = 1;

        return new CommandResult(true);
    }
}
