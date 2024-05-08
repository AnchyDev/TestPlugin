using BepInEx.Logging;

using HarmonyLib;

using System.Linq;

using TestPlugin.Commands;

namespace TestPlugin.Patches;

[HarmonyPatch(typeof(ChatMessage))]
[HarmonyPatch("Send")]
class SendChatMessagePatch
{
    static void Postfix(ChatMessage __instance, string message)
    {
        if(string.IsNullOrEmpty(message))
        {
            return;
        }

        if(!message.StartsWith(CommandHandler.TRIGGER.ToString()))
        {
            return;
        }

        var args = message.Split(' ');
        var command = args[0].Split(CommandHandler.TRIGGER)[1];

        var handler = CommandHandler.Instance;
        var result = handler.Process(command, args);

        var logger = (ManualLogSource)Logger.Sources.First(l => l.SourceName == PluginInfo.PLUGIN_NAME);

        if (!result.Result)
        {
            var error = string.IsNullOrEmpty(result.Message) ? 
                $"An unknown error occured while executing command '{command}'." : 
                $"An error occured while executing command '{command}': {result.Message}";

            logger.LogError(error);

            if(result.SendToPlayer)
            {
                GameManager.ChatScreenInput.AddText(error, UnityEngine.Color.red);
            }

            return;
        }

        logger.LogInfo($"Player '{__instance?.Sender?.CharacterName}' executed command '{command}'.");

        if (result.SendToPlayer && 
            result.Message is not null)
        {
            GameManager.ChatScreenInput.AddText(result.Message);
        }
    }
}
