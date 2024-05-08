using UnityEngine;

namespace TestPlugin.Commands;

internal class WeatherCommand : ICommand
{
    public string Command => "weather";
    public string Description => "Sets the current weather.";

    public CommandResult Process(string command, string[] args)
    {
        var natureComponent = GameObject.FindObjectOfType<Nature>();
        if (!natureComponent)
        {
            return new CommandResult(false);
        }

        if (args.Length < 2)
        {
            return new CommandResult(false, "Missing weather argument. Usage: time <clear/lightrain/rain/heavyrain | number>");
        }

        var value = args[1];

        if (int.TryParse(value, out int weatherValue))
        {
            natureComponent.SetWeatherPattern(weatherValue);
        }
        else
        {
            switch (value.ToLower())
            {
                case "clear":
                    natureComponent.SetWeatherPattern(-1);
                    break;

                case "lightrain":
                    natureComponent.SetWeatherPattern(1);
                    break;

                case "rain":
                    natureComponent.SetWeatherPattern(2);
                    break;

                case "heavyrain":
                    natureComponent.SetWeatherPattern(3);
                    break;

                default:
                    return new CommandResult(false, "Invalid value.");
            }
        }

        return new CommandResult(true);
    }
}
