namespace TestPlugin.Commands;

public class CommandResult
{
    public bool Result { get; set; }
    public string? Message { get; set; }
    public bool SendToPlayer { get; set; }

    /// <summary>
    /// The result of the executed command.
    /// </summary>
    /// <param name="result">The result of the executed command.</param>
    /// <param name="message">The message logged to console and sent to player.</param>
    /// <param name="sendToPlayer">Send the message to player.</param>
    public CommandResult(bool result, string? message = "", bool sendToPlayer = true)
    {
        Result = result;
        Message = message;
        SendToPlayer = sendToPlayer;
    }
}
