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

        if(args.Length < 2)
        {
            return new CommandResult(false, "Missing time argument. Usage: time <day/night, 0-1>");
        }

        var value = args[1];

        if(float.TryParse(value, out float floatTime))
        {
            natureComponent.SetWeatherPattern(-1);
            natureComponent.DayNight.TimeOfDay = floatTime;
        }
        else
        {
            switch (value.ToLower())
            {
                case "day":
                    natureComponent.DayNight.TimeOfDay = 0;
                    break;

                case "night":
                    natureComponent.DayNight.TimeOfDay = 0.5f;
                    break;

                default:
                    return new CommandResult(false, "Invalid value.");
            }
        }

        natureComponent.SetWeatherPattern(-1);

        return new CommandResult(true);
    }
}
