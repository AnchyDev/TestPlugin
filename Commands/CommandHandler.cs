using System.Collections.Generic;

namespace TestPlugin.Commands;

public class CommandHandler
{
    public const char TRIGGER = '.';

    private static CommandHandler? instance;
    public static CommandHandler Instance
    {
        get
        {
            if(instance is null)
            {
                instance = new CommandHandler();
            }

            return instance;
        }
    }

    public Dictionary<string, ICommand> Commands { get; private set; }

    public CommandHandler()
    {
        Commands = new Dictionary<string, ICommand>();
    }

    public void RegisterCommand(ICommand command)
    {
        if(Commands.ContainsKey(command.Command))
        {
            Commands.Remove(command.Command);
        }

        Commands.Add(command.Command, command);
    }

    public CommandResult Process(string command, string[] args)
    {
        if(!Commands.ContainsKey(command))
        {
            return new CommandResult(false, $"Command not found.");
        }

        return Commands[command].Process(command, args);
    }
}
