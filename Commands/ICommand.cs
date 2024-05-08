namespace TestPlugin.Commands;

public interface ICommand
{
    public string Command { get; }
    public string Description { get; }

    public CommandResult Process(string command, string[] args);
}
